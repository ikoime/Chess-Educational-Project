using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Educational_Project
{
    /// <summary>
    /// The class containing the board and their appropriate helper functions.
    /// </summary>
    public class Board
    {
        public Observable2dArray<Piece> board;
        /// <summary>
        /// The default constructor. Assumes a normal game, so creates an 8x8 board with a default starting position.
        /// </summary>
        public Board()
        {
            this.board = new Observable2dArray<Piece>(8, 8);
            //Setup default pieces.
        }
        /// <summary>
        /// Creates a board of a defined size.
        /// </summary>
        /// <param name="x"><inheritdoc cref="Observable2dArray{T}.Observable2dArray(int, int)" path="/param[@name='x']"/></param>
        /// <param name="y"><inheritdoc cref="Observable2dArray{T}.Observable2dArray(int, int)" path="/param[@name='y']"/></param>
        public Board(int x, int y)
        {
           this.board = new Observable2dArray<Piece>(x, y);
        }
        /// <summary>
        /// Creates a board from a pre-created board.
        /// </summary>
        /// <param name="board">The board being copied.</param>
        public Board(Observable2dArray<Piece> board)
        {
            this.board = board;
        }
    }
}
