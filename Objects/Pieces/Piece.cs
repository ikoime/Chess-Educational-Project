using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Educational_Project
{
    /// <summary>
    /// The enum for which represents the values for the piece colours.
    /// While this is transparent to the compiler, it helps with the readability and consistency of the code. 
    /// </summary>
    public enum PieceColour
    {
        White = 0,
        Black = 1
    }
    /// <summary>
    /// The abstract parent class for each of the pieces. 
    /// </summary>
    public abstract class Piece
    {
        /// <summary>
        /// The position of the piece on the board.
        /// </summary>
        public Vector2 pos;

        /// <summary>
        /// The colour of the piece. 
        /// </summary>
        public PieceColour colour;

        /// <summary>
        /// A function which returns a list of valid moves.
        /// </summary>
        /// <param name="board">The parent board which the piece is placed on.</param>
        public abstract void GetMoves(Board board);
    }
    
    


}
