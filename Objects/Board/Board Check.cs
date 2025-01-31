using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Educational_Project
{
    public partial class Board
    {
        public bool TurnPlayerInCheck(PieceColour turnColour)
        {
            Square kingSquare = null;
            if (turnColour == PieceColour.White)
            {
                kingSquare = this[whiteKingPos];
            }
            else
            {
                kingSquare = this[blackKingPos];
            }


            (Vector2, Type)[] rayChecks = {(new Vector2(0,1),typeof(Rook)),
                                           (new Vector2(1,0),typeof(Rook)),
                                           (new Vector2(0,-1),typeof(Rook)),
                                           (new Vector2(-1,0),typeof(Rook)),

                                           (new Vector2(1,1),typeof(Bishop)),
                                           (new Vector2(-1,1),typeof(Bishop)),
                                           (new Vector2(1,-1),typeof(Bishop)),
                                           (new Vector2(-1,-1),typeof(Bishop))};


            foreach ((Vector2, Type) rayCheck in rayChecks)
            {
                if (CheckRayCast(rayCheck.Item2, rayCheck.Item1, kingSquare.pos, kingSquare.piece.colour))
                {
                    return true;
                }
            }

            for (int xAdd = -1; xAdd <= 1; xAdd += 2)
            {
                for (int yAdd = -1; yAdd <= 1; yAdd += 2)
                {
                    //Then, for each diagonal square, we move by the xAdd again.
                    Vector2 targetPos = new Vector2(kingSquare.pos.X + 2 * xAdd, kingSquare.pos.Y + yAdd);
                    if (IsOnBoard(targetPos))
                    {
                        if (this[targetPos].piece is Knight && this[targetPos].piece.colour != turnColour)
                        {
                            return true;
                        }
                    }


                    targetPos = new Vector2(kingSquare.pos.X + xAdd, kingSquare.pos.Y + 2 * yAdd);
                    if (IsOnBoard(targetPos))
                    {
                        if (this[targetPos].piece is Knight && this[targetPos].piece.colour != turnColour)
                        {
                            return true;
                        }
                    }
                }
            }
            int directionSign;
            if (kingSquare.piece.colour == PieceColour.White)
            {
                directionSign = -1;
            }
            else
            {
                directionSign = 1;
            }
            for (int i = -1; i < 2; i += 2)
            {
                Vector2 targetPos = kingSquare.pos + new Vector2(i, directionSign);
                if (IsOnBoard(targetPos) && this[targetPos].piece is Pawn && this[targetPos].piece.colour != turnColour)
                {
                    return true;
                }
            }


            return false;


        }
        private bool CheckRayCast(Type targetType, Vector2 dir, Vector2 pos, PieceColour colour)
        {
            int mag = 1;
            while (true)
            {
                Vector2 targetPos = pos + mag * dir;
                if (!IsOnBoard(targetPos))
                {
                    return false;
                }

                MoveType moveType = MoveTypeForMove(colour, targetPos);
                //If there is a piece blocking
                if (moveType == MoveType.Invalid)
                {
                    return false;
                }


                if (moveType == MoveType.Capture && (this[targetPos].piece.GetType() == targetType || this[targetPos].piece.GetType() == typeof(Queen)))
                {
                    return true;
                }
                else if (moveType == MoveType.Capture)
                {
                    return false;
                }
                mag++;
            }
        }
    }
}
