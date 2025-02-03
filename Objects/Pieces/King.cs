
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
            //castling
            if (hasMoved == false && !board.isInCheck)
            {

                if (board[7, (int)pos.Y].piece is not null && board[7, (int)pos.Y].piece.hasMoved == false && CheckRow(board, (int)pos.X, 7, (int)pos.Y))
                {
                    bool shortCastlingValid = true;
                    for (int i = 1; i <= 2; i++)
                    {
                        if (board.PlayerInCheck(pos + new Vector2(i, 0), colour))
                        {
                            shortCastlingValid = false;
                        }
                    }

                    if (shortCastlingValid)
                    {
                        validMoves.Add(new Move(pos, new Vector2(pos.X + 2, pos.Y),MoveType.ShortCastling));
                    }
                    
                }
                if (board[1, (int)pos.Y].piece is not null && board[1, (int)pos.Y].piece.hasMoved == false && CheckRow(board, 1,(int)pos.X, (int)pos.Y))
                {
                    bool longCastlingValid = true;
                    for (int i = 1; i <= 2; i++)
                    {
                        if (board.PlayerInCheck(pos + new Vector2(-i, 0), colour))
                        {
                            longCastlingValid = false;
                        }
                    }

                    if (longCastlingValid)
                    {
                        validMoves.Add(new Move(pos, new Vector2(pos.X - 2, pos.Y), MoveType.LongCastling));
                    }
                    
                }
            }



            return validMoves;
        }
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
        
    }
   
   
}
