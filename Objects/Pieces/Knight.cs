
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
            for (int xAdd = -1; xAdd <= 1; xAdd += 2)
            {
                for (int yAdd = -1; yAdd <= 1; yAdd += 2)
                {
                    Vector2 targetPos = new Vector2(pos.X + 2 * xAdd, pos.Y + yAdd);
                    MoveLogic(board, pos, targetPos, ref validMoves);
                   

                    
                    targetPos = new Vector2(pos.X + xAdd, pos.Y + 2 * yAdd);
                    MoveLogic(board, pos, targetPos, ref validMoves);
                }
            }
            return validMoves;
        }
        private void MoveLogic(Board board,Vector2 pos, Vector2 targetPos, ref List<Move> validMoves)
        {
            if (board.IsOnBoard(targetPos))
            {
               
                MoveType moveType = board.MoveTypeForMove(colour, targetPos);
                if (moveType != MoveType.Invalid)
                {
                    validMoves.Add(new Move(pos, targetPos, moveType));
                }
            }
        }
    }
}
