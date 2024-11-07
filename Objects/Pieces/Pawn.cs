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
        public override void GetMoves(Board board, Vector2 pos)
        {
            throw new NotImplementedException();
        }
    }
}
