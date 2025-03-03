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
        public void Move(Move move, bool testing)
        {
            if (this[move.startPosition].piece is King)
            {
                if (this[move.startPosition].piece.colour == PieceColour.White)
                {
                    whiteKingPos = move.endPosition;
                }
                else
                {
                    blackKingPos = move.endPosition;
                }
            }
            if (move.moveType == MoveType.ShortCastling)
            {
                //Moving the rook to the correct square.
                Square rookSquare = this[(int)move.startPosition.Y, 7];
                this[move.endPosition - new Vector2(1, 0)].piece = rookSquare.piece;
                rookSquare.piece = null;
            }
            else if (move.moveType == MoveType.LongCastling)
            {
                //Moving the rook to the correct square.
                Square rookSquare = this[(int)move.startPosition.Y, 0];
                this[move.endPosition + new Vector2(1, 0)].piece = rookSquare.piece;
                rookSquare.piece = null;
            }
            else if (move.moveType == MoveType.EnPassant)
            {
                this[lastPlayedMove.endPosition].piece = null;
            }
            else if ((move.moveType == MoveType.Promotion || move.moveType == MoveType.PromotionCapture) && !testing)
            {
                Promotion promotion = new Promotion(this[move.startPosition].piece.colour);
                promotion.ShowDialog();
                (Type, PieceColour) chosenValue = promotion.chosenType;
                this[move.startPosition].piece = PromotionSwitch(chosenValue);

            }

            Square startingSquare = this[move.startPosition];
            this[move.endPosition].piece = startingSquare.piece;
            startingSquare.piece = null;

            //Recording the move for en passant.
            lastPlayedMove = move;
        }

        /// <summary>
        /// A function that converts the chosen piece on the promotion window into a piece object.
        /// </summary>
        /// <param name="value">The tuple returned from the window.</param>
        /// <returns>A piece of the correct type and colour.</returns>
        private Piece PromotionSwitch((Type, PieceColour) value)
        {
            Type pieceType = value.Item1;
            if (pieceType == typeof(Queen))
            {
                return new Queen(value.Item2);
            }
            else if (pieceType == typeof(Bishop))
            {
                return new Bishop(value.Item2);
            }
            else if (pieceType == typeof(Knight))
            {
                return new Knight(value.Item2);
            }
            else
            {
                return new Rook(value.Item2);
            }
        }
    }
}
