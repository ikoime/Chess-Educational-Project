
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Educational_Project
{
    public class King : Piece
    {
        ///<inheritdoc cref="Pawn(PieceColour)"/>
        public King(PieceColour colour)
        {
            this.colour = colour;
        }
        ///<inheritdoc/>
        public override List<Move> GetMoves(Board board, Vector2 pos)
        {
            List<Move> validMoves = new List<Move>();
            //Iterates the x and y variables to check in a 3x3 square around the king.
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    //If x and y are both zero, we don't need to check that square since its the original square.
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }
                    //Calculating the position we are checking. 
                    Vector2 targetPos = new Vector2(pos.X+x,pos.Y+y);
                    //Checking if that square is on the board.
                    if (board.IsOnBoard(targetPos))
                    {
                        //If it is, we check the moveType of that move. This returns a result from the enum.
                        MoveType moveType = board.MoveTypeForMove(colour, targetPos);
                        if (!(moveType == MoveType.Invalid))
                        {
                            //If the move is not invalid we add it to the list of valid moves.
                            //A moveType of invalid simply means a piece of the same colour is on that square.
                            validMoves.Add(new Move(pos,targetPos,moveType));
                        }
                    }
                    

                }
            }
            //If the king hasn't moved yet and is not in check, then I need to see if they can castle.
            if (hasMoved == false && !board.isInCheck)
            {
                //Firstly, we see if we can short castle.
                Vector2 rookPos = new Vector2(7, (int)pos.Y);
                if (IsCastlingLegal(board, rookPos,pos,(int)pos.X, 7, MoveType.ShortCastling))
                {
                    //If we can, we add the move to the list of legal moves.
                    validMoves.Add(new Move(pos, new Vector2(pos.X + 2, pos.Y), MoveType.ShortCastling));
                }

                //Then, we see if we can long castle.
                rookPos.X = 0;
                if (IsCastlingLegal(board, rookPos, pos, 1, (int)pos.X, MoveType.LongCastling))
                {
                    //If we can, we add the move to the list of legal moves.
                    validMoves.Add(new Move(pos, new Vector2(pos.X - 2, pos.Y), MoveType.LongCastling));
                }
            }



            return validMoves;
        }
        /// <summary>
        /// A function which checks if there are no pieces between two specified positions.
        /// </summary>
        /// <param name="board"><inheritdoc cref="IsCastlingLegal(Board, Vector2, Vector2, int, int, MoveType)"/></param>
        /// <param name="start"><inheritdoc cref="IsCastlingLegal(Board, Vector2, Vector2, int, int, MoveType)"/></param>
        /// <param name="end"><inheritdoc cref="IsCastlingLegal(Board, Vector2, Vector2, int, int, MoveType)"/></param>
        /// <param name="rank"><inheritdoc cref="IsCastlingLegal(Board, Vector2, Vector2, int, int, MoveType)"/></param>
        /// <returns>True if its empty, false else.</returns>
        internal bool CheckRow(Board board, int start, int end, int rank)
        {
            for (int i = start+1; i < end; i++)
            {
                if (board[rank,i].piece is not null)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// All of the internal logic to see if the king can castle in a given direction.
        /// </summary>
        /// <param name="board">The instance of board to check.</param>
        /// <param name="rookPos">The rooks position.</param>
        /// <param name="kingPos">The kings position.</param>
        /// <param name="start">The start position to perform a row check.</param>
        /// <param name="end">The end position for the row check.</param>
        /// <param name="castlingDirection">The type of castling, either short or long.</param>
        /// <returns>True if castling is legal, false else.</returns>
        internal bool IsCastlingLegal(Board board, Vector2 rookPos,Vector2 kingPos, int start, int end,MoveType castlingDirection)
        {
            //If the rook is not there, castling is not possible.
            if (board[rookPos].piece is null)
            {
                return false;
            }
            //If the rook has moved, castling is not possible.
            if (board[rookPos].piece.hasMoved)
            {
                return false;
            }
            //If the row is not empty, castling is not possible.
            if (!CheckRow(board, start, end, (int)kingPos.Y))
            {
                return false;
            }
            
            //Determines the castling direction.
            int directionSign = 1;
            if (castlingDirection == MoveType.LongCastling)
            {
                directionSign = -1;
            }
            
            for (int i = 1; i <= 2; i++)
            {
                //Since you can't castle through check, I need to simulate a fake king on each of these squares.
                //If any of these fake kings are in check, castling is not possible.
                if (board.PlayerInCheck(kingPos + new Vector2(directionSign * i, 0), colour))
                {
                    return false;
                }
            }
            return true;
        }
        
    }
   
   
}
