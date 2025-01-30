using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Educational_Project
{
    public class Pawn : Piece
    {
        /// <summary>
        /// The base constructor.
        /// </summary>
        /// <param name="colour"><inheritdoc cref="Piece.colour"/></param>
        public Pawn(PieceColour colour)
        {
            this.colour = colour;
        }
        ///<inheritdoc/>
        public override List<Move> GetMoves(Board board, Vector2 pos)
        {
            List<Move> legalMoves = new List<Move>();
            int directionSign;
            int startingRank;
            int endRank;
            PieceColour enemyColour;
            //Determining the move direction and the ype of the opposing coloured pieces.
            if (this.colour == PieceColour.Black)
            {
                directionSign = 1;//board.turnColour == PieceColour.White ? 1 : -1;
                startingRank = 1;//board.turnColour == PieceColour.White ? 1 : board.pieces.Count - 2;
                endRank = board.pieces.Count - 1;
                enemyColour = PieceColour.White;
            }
            else
            {
                directionSign = -1; //board.turnColour == PieceColour.White ? 1 : -1;
                startingRank = board.pieces.Count - 2;//board.turnColour == PieceColour.White ? board.pieces.Count - 2 : 1;
                endRank = 0;
                enemyColour = PieceColour.Black;
            }
            int x = (int)pos.X;
            int y = (int)pos.Y;
            Vector2 targetPos;
            //If it can move forward.
            if (board[y+directionSign,x].piece is null)
            {
                targetPos = new Vector2(x, y + directionSign);
                if ((y + directionSign) == endRank)
                {
                    legalMoves.Add(new Move(pos, targetPos, MoveType.Promotion));
                }
                else
                {
                    legalMoves.Add(new Move(pos, new Vector2(x, y + directionSign)));
                }
                
                //If it hasn't moved yet. 
                if (y == startingRank)
                {
                    if (board[y + directionSign * 2, x].piece is null)
                    {
                        legalMoves.Add(new Move(pos, new Vector2(x, y + directionSign * 2),MoveType.FirstPawnMove));
                    }
                }
            }
            //If there is an opposing piece to capture to the right.
            targetPos = new Vector2(x + 1, y + directionSign);
            if (x < board.pieces.Count - 1 && board.MoveTypeForMove(colour,targetPos) == MoveType.Capture)
            {
                if ((y+directionSign) == endRank)
                {
                    legalMoves.Add(new Move(pos, targetPos, MoveType.PromotionCapture));
                }
                else
                {
                    legalMoves.Add(new Move(pos, targetPos, MoveType.Capture));
                }
               
               
            }
            //If there is an opposing piece to capture to the left.
            targetPos = new Vector2(x-1,y+ directionSign);
            if (x > 0 && board.MoveTypeForMove(colour,targetPos) == MoveType.Capture)
            {
                if ((y+directionSign)== endRank)
                {
                    legalMoves.Add(new Move(pos,targetPos, MoveType.PromotionCapture));
                }
                else
                {
                    legalMoves.Add(new Move(pos, new Vector2(x - 1, y + directionSign), MoveType.Capture));
                }
                
            }
            
            //En passant.
            if (board.lastPlayedMove.moveType == MoveType.FirstPawnMove && Math.Abs(x - board.lastPlayedMove.endPosition.X) == 1 && y == board.lastPlayedMove.endPosition.Y)
            {
                legalMoves.Add(new Move(pos, new Vector2(board.lastPlayedMove.endPosition.X, pos.Y + directionSign),MoveType.EnPassant));
            }
            
            return legalMoves;
           

        }
    }
}
