
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
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }
                    Vector2 targetPos = new Vector2(pos.X+x,pos.Y+y);
                    if (board.IsOnBoard(targetPos))
                    {
                        MoveType moveType = board.MoveTypeForMove(colour, targetPos);
                        if (!(moveType == MoveType.Invalid))
                        {
                            validMoves.Add(new Move(pos,targetPos,moveType));
                        }
                    }
                    

                }
            }
            return validMoves;
        }
    }
}
