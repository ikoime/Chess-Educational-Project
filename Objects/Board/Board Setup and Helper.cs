using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace Chess_Educational_Project
{
    /// <summary>
    /// The class containing the board and their appropriate helper functions.
    /// </summary>
    public partial class Board
    {


        private void Setup()
        {
            for (int i = 0; i < xSize; i++)
            {
                pieces.Add(new ObservableCollection<Square>());
                for (int j = 0; j < ySize; j++)
                {
                    //Adding elements to our enumerable to simulate array indexing.
                    pieces[i].Add(new Square(new Vector2(j, i)));
                }
            }
        }
        /// <summary>
        /// Adds the default starting position to the board.
        /// </summary>
        /// <exception cref="Exception">This exception will be ran if the xSize and ySize aren't 8.</exception>
        public void DefaultLayout()
        {
            if (xSize == 8 && ySize == 8)
            {
                for (int i = 1; i < 8; i += 5)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        this[i, j].piece = new Pawn(i == 6 ? PieceColour.White : PieceColour.Black);
                    }
                }
                for (int i = 0; i < 8; i += 7)
                {
                    PieceColour pieceColour = (i == 7 ? PieceColour.White : PieceColour.Black);
                    this[i, 0].piece = new Rook(pieceColour);
                    this[i, 7].piece = new Rook(pieceColour);
                    this[i, 1].piece = new Knight(pieceColour);
                    this[i, 6].piece = new Knight(pieceColour);
                    this[i, 2].piece = new Bishop(pieceColour);
                    this[i, 5].piece = new Bishop(pieceColour);
                    this[i, 3].piece = new Queen(pieceColour);
                    this[i, 4].piece = new King(pieceColour);
                }
            }
            else
            {
                throw new Exception("The xSize and ySize must be 8 to perform a default setup");
            }
        }
        /// <summary>
        /// Checks if a square is within the bounds of the board.
        /// </summary>
        /// <param name="pos">The position to be checked.</param>
        /// <returns>True if the square is on the board, false else.</returns>
        public bool IsOnBoard(Vector2 pos)
        {
            if (pos.X < 0 || pos.X >= xSize)
            {
                return false;
            }
            else if (pos.Y < 0 || pos.Y >= ySize)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Returns the if the move is a capture or not.
        /// </summary>
        /// <param name="colour">The colour of the player making the move.</param>
        /// <param name="pos">The end position of the move.</param>
        /// <returns>Either MoveType.Capture if its a capture, or MoveType.Default else.</returns>
        public MoveType MoveTypeForMove(PieceColour colour, Vector2 pos)
        {
            PieceColour enemyColour = (colour == PieceColour.White ? PieceColour.Black : PieceColour.White);

            if (this[pos].piece is null)
            {
                return MoveType.Default;
            }
            else if (this[pos].piece.colour == enemyColour)
            {
                return MoveType.Capture;
            }
            //Temp
            return MoveType.Invalid;
        }
        /// <summary>
        /// Alternates the turn colour between white and black.
        /// </summary>
        public void AlternateTurnOrder()
        {
            if (this.turnColour == PieceColour.White)
            {
                this.turnColour = PieceColour.Black;
                return;
            }
            this.turnColour = PieceColour.White;
        }
        /// <summary>
        /// Used for the movement of the bishop, rook and queen since they all check along a given direction vector.
        /// </summary>
        /// <param name="colour">The colour of the piece whose moves are being checked.</param>
        /// <param name="pos">The position of the piece whose moves are being checked.</param>
        /// <param name="direction">The direction vector to check along</param>
        /// <param name="validMoves">The valid moves list to append the results to.</param>
        public void LinearMove(PieceColour colour, Vector2 pos, Vector2 direction, ref List<Move> validMoves)
        {
            //Declaring the starting "extension" or magnitude (in the case of vertical or horizontal vectors)
            int mag = 1;
            while (true)
            {
                //Computing the position to check. This is the starting position + the extended direction vector.
                Vector2 targetPos = pos + mag * direction;
                //If this move isn't on the board, there isn't any reason to check anymore in this direction so we terminate the loop here.
                if (!IsOnBoard(targetPos))
                {
                    break;
                }
                //We then compute the MoveType for this move.
                MoveType moveType = MoveTypeForMove(colour, targetPos);
                //If the move targets an allied piece, there isn't any reason to check in this direction anymore since you can't travel through pieces.
                if (moveType == MoveType.Invalid)
                {
                    break;
                }
                //We then add the move, since in all other cases we should.
                validMoves.Add(new Move(pos, targetPos, moveType));
                //However, if the move targets an enemy piece, we should still terminate the loop due to being unable to move through pieces.
                if (moveType == MoveType.Capture)
                {
                    break;
                }
                //Incrementing the magnitude to look further along this direction.
                mag++;
            }
        }

       

    }
}
