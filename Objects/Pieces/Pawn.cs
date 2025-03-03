using Microsoft.VisualBasic;
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
            int endRank;
            int startRank;
            PieceColour enemyColour;
            //Determining the move direction and the ype of the opposing coloured pieces.
            if (this.colour == PieceColour.Black)
            {
                directionSign = 1;//board.turnColour == PieceColour.White ? 1 : -1;
                startRank = 1;
                endRank = board.pieces.Count - 1;
                enemyColour = PieceColour.White;
            }
            else
            {
                directionSign = -1; //board.turnColour == PieceColour.White ? 1 : -1;
                startRank = board.pieces.Count - 2;
                endRank = 0;
                enemyColour = PieceColour.Black;
            }

            
            int x = (int)pos.X;
            int y = (int)pos.Y;
            Vector2 targetPos;
            MoveType typeOfMove = MoveType.Default;
            MoveType captureType = MoveType.Capture;
            //Deciding if promotion is legal.
            bool canPromote = (y + directionSign) == endRank;
            if (canPromote)
            {
                typeOfMove = MoveType.Promotion;
                captureType = MoveType.PromotionCapture;
            }

            //If it can move forward.
            if (board[y + directionSign, x].piece is null)
            {
                targetPos = new Vector2(x, y + directionSign);
                legalMoves.Add(new Move(pos, targetPos, typeOfMove));

                //If it hasn't moved yet. 
                if (y == startRank)
                {
                    if (board[y + directionSign * 2, x].piece is null)
                    {
                        legalMoves.Add(new Move(pos, new Vector2(x, y + directionSign * 2), MoveType.FirstPawnMove));
                    }
                }
            }



            //If there is an opposing piece to capture to the right.
            targetPos = new Vector2(x + 1, y + directionSign);
            if (x < board.pieces.Count - 1 && board.MoveTypeForMove(colour,targetPos) == MoveType.Capture)
            {
                legalMoves.Add(new Move(pos, targetPos, captureType));
            }
            //If there is an opposing piece to capture to the left.
            targetPos = new Vector2(x-1,y+ directionSign);
            if (x > 0 && board.MoveTypeForMove(colour,targetPos) == MoveType.Capture)
            {
                legalMoves.Add(new Move(pos, targetPos, captureType));
            }
            
            //En passant.
            if (IsEnPassantLegal(board,pos))
            {
                legalMoves.Add(new Move(pos, new Vector2(board.lastPlayedMove.endPosition.X, pos.Y + directionSign),MoveType.EnPassant));
            }
            
            return legalMoves;
           

        }
        /// <summary>
        /// Checks if en passant is legal for a given pawn position and board state.
        /// </summary>
        /// <param name="board">The instance of board.</param>
        /// <param name="pawnPos">The position of the pawn whose legal moves are being checked.</param>
        /// <returns>True if enPassant is legal, false else</returns>
        internal bool IsEnPassantLegal(Board board, Vector2 pawnPos)
        {
            //If the last played move didn't move a pawn two squares, en passant shouldn't be allowed.
            if (board.lastPlayedMove.moveType != MoveType.FirstPawnMove)
            {
                return false;
            }
            //If the two pawns aren't next to each other on the x axis, then en passant shouldn't be allowed.
            if (Math.Abs(pawnPos.X - board.lastPlayedMove.endPosition.X) != 1)
            {
                return false;
            }
            //If the two pawns don't have the same y coordinate, then en passant shouldn't be allowed.
            if ((int)pawnPos.Y != board.lastPlayedMove.endPosition.Y)
            {
                return false;
            }
            return true;
        }
    }
}
