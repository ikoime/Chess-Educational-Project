
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Educational_Project
{
    public class Bishop : Piece
    {
        ///<inheritdoc cref="Pawn(PieceColour)"/>
        public Bishop(PieceColour colour)
        {
            this.colour = colour;
        }
        ///<inheritdoc/>
        public override List<Move> GetMoves(Board board, Vector2 pos)
        {
            List<Move> validMoves = new List<Move>();

            //Checks in each of the diagonal directions.
            board.LinearMove(colour,pos, new Vector2(1, 1), ref validMoves);
            board.LinearMove(colour, pos, new Vector2(1, -1), ref validMoves);
            board.LinearMove(colour, pos, new Vector2(-1, 1), ref validMoves);
            board.LinearMove(colour, pos, new Vector2(-1, -1), ref validMoves);

            return validMoves;

        }
        
           
    }
}
