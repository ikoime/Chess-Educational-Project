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
            List<Move> moves = new List<Move>();
            int directionSign;
            int startingRank;
            PieceColour enemyColour;
            //Determining the move direction and the ype of the opposing coloured pieces.
            if (this.colour == PieceColour.Black)
            {
                directionSign = 1;
                startingRank = 1;
                enemyColour = PieceColour.White;
            }
            else
            {
                directionSign = -1;
                startingRank = board.pieces.Count - 2;
                enemyColour = PieceColour.Black;
            }
            int x = (int)pos.X;
            int y = (int)pos.Y; 
            //If it can move forward.
            if (board[y+directionSign,x].piece is null)
            {
                moves.Add(new Move(pos, new Vector2(x, y + directionSign)));
                //If it hasn't moved yet. 
                if (y == startingRank)
                {
                    if (board[y + directionSign * 2, x].piece is null)
                    {
                        moves.Add(new Move(pos, new Vector2(x, y + directionSign * 2)));
                    }
                }
            }
            //If there is an opposing piece to capture to the right.
            if (x < board.pieces.Count - 1 && board[y + directionSign, x + 1].piece is not null && board[y+directionSign,x+1].piece.colour == enemyColour)
            {
                moves.Add(new Move(pos, new Vector2(x + 1, y + directionSign), MoveType.Capture));
            }
            //If there is an opposing piece to capture to the left.
            if (x > 0 && board[y + directionSign, x - 1].piece is not null && board[y+directionSign,x-1].piece.colour == enemyColour)
            {
                moves.Add(new Move(pos, new Vector2(x - 1, y + directionSign), MoveType.Capture));
            }
            
            
            
            return moves;
           

        }
    }
}
