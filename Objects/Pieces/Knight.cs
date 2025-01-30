
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Educational_Project 
{
    public class Knight : Piece
    {
        ///<inheritdoc cref="Pawn(PieceColour)"/>
        public Knight(PieceColour colour)
        {
            this.colour = colour;
        }
        ///<inheritdoc/>
        public override List<Move> GetMoves(Board board, Vector2 pos)
        {
            List<Move> validMoves = new List<Move>();
            //These two for loops check the squares diagonally one square from the knights position.
            for (int xAdd = -1; xAdd <= 1; xAdd += 2)
            {
                for (int yAdd = -1; yAdd <= 1; yAdd += 2)
                {
                    //Then, for each diagonal square, we move by the xAdd again.
                    Vector2 targetPos = new Vector2(pos.X + 2 * xAdd, pos.Y + yAdd);
                    MoveLogic(board, pos, targetPos, ref validMoves);
                   

                    //This is then repeated for yAdd as well.
                    targetPos = new Vector2(pos.X + xAdd, pos.Y + 2 * yAdd);
                    MoveLogic(board, pos, targetPos, ref validMoves);
                }
            }
            return validMoves;
        }
        /// <summary>
        /// Repeated movement logic for the knight. Never used outside of the GetMoves function. 
        /// </summary>
        /// <param name="board"><inheritdoc cref="GetMoves(Board, Vector2)"/></param>
        /// <param name="pos"><inheritdoc cref="GetMoves(Board, Vector2)"/></param>
        /// <param name="targetPos">The target position of the move</param>
        /// <param name="validMoves">A list of valid moves.</param>
        internal void MoveLogic(Board board,Vector2 pos, Vector2 targetPos, ref List<Move> validMoves)
        {
            
            //If the target move is on the board.
            if (board.IsOnBoard(targetPos))
            {
                //Determine if the move is a capture, check etc.
                MoveType moveType = board.MoveTypeForMove(colour, targetPos);
                //If the move doesn't target an allied piece.
                if (moveType != MoveType.Invalid)
                {
                    //Then we add the move to the list of valid moves.
                    validMoves.Add(new Move(pos, targetPos, moveType));
                }
            }
        }
    }
}
