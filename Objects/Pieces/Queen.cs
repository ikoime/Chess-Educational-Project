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
        ///<inheritdoc cref="Pawn(Vector2, PieceColour)"/>
        public Queen(Vector2 pos, PieceColour colour)
        {
            this.pos = pos;
            this.colour = colour;
        }
        ///<inheritdoc/>
        public override void GetMoves(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
