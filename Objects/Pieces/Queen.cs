using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Educational_Project
{
    public class Queen : Piece
    {
        ///<inheritdoc cref="Pawn(PieceColour)"/>
        public Queen(PieceColour colour)
        {
            this.colour = colour;
        }
        ///<inheritdoc/>
        public override void GetMoves(Board board, Vector2 pos)
        {
            throw new NotImplementedException();
        }
    }
}
