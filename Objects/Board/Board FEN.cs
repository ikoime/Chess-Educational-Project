using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace Chess_Educational_Project
{
    public partial class Board  
    {
        /// <summary>
        /// The logic for the conversion between a FEN and a board state.
        /// </summary>
        /// <param name="fen"><inheritdoc cref="Board(string fen)" path="fen"/></param>
        /// <returns>A boolean indicating success in parsing.</returns>
        public bool FENToBoard(string fen)
        {
            //Splitting the FEN into its 6, constituent segments since each of these are separated by spaces.
            string[] fenSplit = fen.Split(' ');
            int y = 0;

            //Iterating through each of the rows in the FEN.
            //This is each row on the board is separated by dashes.
            foreach (string row in fenSplit[0].Split("/"))
            {
                //Setting the x value to 0 at the start of each row.
                int x = 0;

                //Iterating through all of the characters in that row.
                foreach (char c in row)
                {
                    //If the character is a number, it represents that many empty spaces.
                    if (char.IsDigit(c))
                    {
                        //Subtracting the ASCII value of the 0 from the ASCII value of the number.
                        //This is because Convert.ToInt16(char) returns the ASCII value, and each of the number characters have consecutive ASCII values.
                        //Therefore, subtracting '0' from the ASCII value for a number will give its offset in the character set from 0, which is just the number.
                        int num = Convert.ToInt16(c) - '0';

                        //Increment the x coordinate by the number of blank spaces.
                        x += num;
                    }
                    else
                    {
                        //Checks if the dictionary contains a piece that corresponds to that character.
                        if (!Resources.pieceNotationToPieceObject.TryGetValue(c, out Piece pieceToAdd))
                        {
                            //If this function returns false, it means the character was unparsable.
                            //Therefore, the FEN is invalid so I return false.
                            return false;
                        }

                        if (pieceToAdd is Rook)
                        {
                            //If the piece is a rook, we assume the rook has moved.
                            //This is for the castling checker later.
                            pieceToAdd.hasMoved = true;
                        }
                        

                        //Adding the piece to the board, and incrementing the x coordinate.
                        this[y, x].piece = pieceToAdd;
                        x++;
                    }


                }
                //Adding one to the y value to keep track of the row count.
                y++;
            }

            //I am expecting this to be either 'w' or 'b'.
            if (fenSplit[1].Length != 1)
            {
                //Therefore, if its not one character, the FEN is invalid.
                //This check is necessary since, otherwise, 'wb' would be valid when it is not.
                return false;
            }

            
            if (!"wb".Contains(fenSplit[1]))
            {
                //If the character is not either 'w' or 'b', the FEN is invalid.
                return false;
            }

            //Sets the turnColour to be the side listed on the FEN.
            //After the previous validation, the character can only be 'w' or 'b'.
            turnColour = PieceColour.White;
            if (fenSplit[1] == "b")
            {
                //If the character is 'b', then its black to move.
                turnColour = PieceColour.Black;
            }

            //This section of the FEN is for castling availability, so I iterate through all of the characters.
            foreach (char c in fenSplit[2])
            {
                //If the character is a -, then no one can castle so I don't need to continue the check.
                if (c == '-')
                {
                    break;
                }
                //This dictionary checks if the character is one of 'KQkq'. If not, TryGetValue returns false.
                //Otherwise, it returns the square of where I would expect the rook to be if castling was legal in that direction.
                if (!Resources.rookPositionsForCastling.TryGetValue(c, out Vector2 rookPosition))
                {
                    //This character was unparsable, so the FEN is invalid.
                    return false;
                }
                //Checks if there is actually a piece there.
                if (this[rookPosition].piece is null)
                {
                    //If not, the FEN is inconsistent and therefore invalid.
                    return false;
                }
                //Checks if this piece is a rook. 
                if (!(this[rookPosition].piece is Rook))
                {
                    //If not, the FEN is inconsistent and therefore invalid.
                    return false;
                }

                //If all of these checks are passed, then we set the hasMoved property of the rook there to false to allow castling.
                this[rookPosition].piece.hasMoved = false;
            }

            //This section is the target square for en passant.
            //The character here should be either '-' or a square in SAN.
            if (!(fenSplit[3] == "-"))
            {
                //If I need to record the previous move, then I need to determine a direction sign.
                //This is the inverse of normal since FENs record the previous move, so if its currently white to move, then black move last.
                int directionSign = -1;
                if (turnColour == PieceColour.White)
                {
                    directionSign = 1;
                }

                //Converts the SAN square to a vector position on the board.
                if (!NotationToPosition(fenSplit[3], out Vector2 pos))
                {
                    //If it was not in correct SAN, the FEN is invalid.
                    return false;
                }

                //If all of these checks are passed, the last move is recorded as a pawn move that 'passed' through the en passant square.
                lastPlayedMove = new Move(pos + new Vector2(0,-directionSign), pos + new Vector2(0, directionSign), MoveType.FirstPawnMove);
            }

            //I expect this to be a numerical character from 0-50.
            if (!int.TryParse(fenSplit[4],out int halfMoves))
            {
                //If it is non-numerical, then the FEN is invalid.
                return false;
            }

            this.halfMoves = halfMoves;
            //Returning true since the FEN parsed successfully.
            return true;
        }
        /// <summary>
        /// A function that converts a square in SAN into a vector position.
        /// </summary>
        /// <param name="notation">The square represented in SAN.</param>
        /// <param name="pos">The converted position.</param>
        /// <returns>A boolean indicating success in parsing.</returns>
        public bool NotationToPosition(string notation, out Vector2 pos)
        {
            //Defining our out parameter in case validation fails.
            pos = new Vector2();

            //The x coordinate is represented as a letter in SAN.
            //Therefore, to convert into a number, the index of the character in 'abcdefgh' is found, which corresponds exactly to that characters x coordinate.
            int x = "abcdefgh".IndexOf(notation[0]);

            //Checks if the character was found in the string.
            if (x == -1)
            {
                //If not, then the SAN is invalid.
                return false;
            }

            //The second character should be a digit.
            if (!char.IsDigit(notation[1]))
            {
                //If it is not, then the SAN is invalid.
                return false;
            }
            //The y coordinate on my board is then flipped. (since white has starting y coordinates 7,6 instead of 0,1).
            //Subtracting the ASCII value of 1 is the same as subtracting '0' then subtracting 1 from the result.
            //This is done to zero index the coordinate system (SAN uses 1-8 rather than 0-7).
            int y = 7 - (Convert.ToInt32(notation[1]) - '1');

            //The converted position vector is then set as the value of the out parameter.
            pos = new Vector2(x, y);

            //True is then returned since the SAN was parsed successfully.
            return true;

        }
    }
}
