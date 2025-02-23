using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Educational_Project
{
    public partial class Board
    {
        /// <summary>
        /// The default constructor. Assumes a normal game, so creates an 8x8 board with a default starting position.
        /// </summary>
        public Board()
        {
            Setup();
            //Setup default pieces.
            DefaultLayout();

            _whiteTime = new TimeSpan(0, 0, 10, 0, 0);
            _blackTime = new TimeSpan(0, 0, 10, 0, 0);


            whiteKingPos = new Vector2(4, 7);
            blackKingPos = new Vector2(4, 0);
        }
        /// <summary>
        /// Creates a board of a defined size.
        /// </summary>
        /// <param name="x">The maximum xSize.</param>
        /// <param name="y">The maximum ySize.</param>
        /// <exception cref="ArgumentException">Is thrown when xSize or ySize <= 0</exception>
        public Board(int x, int y)
        {
            if (x <= 0 || y <= 0)
            {
                throw new ArgumentException("xSize and ySize should be non-zero, positive integers.");
            }
            this.xSize = x;
            this.ySize = y;
            Setup();
        }
        /// <summary>
        /// A copy constructor.
        /// </summary>
        /// <param name="board">The instance to be copied.</param>
        public Board(Board board)
        {
            xSize = board.xSize;
            ySize = board.ySize;

            whiteKingPos = board.whiteKingPos;
            blackKingPos = board.blackKingPos;


            for (int i = 0; i < board.xSize; i++)
            {
                pieces.Add(new ObservableCollection<Square>());
                foreach (Square square in board.pieces[i])
                {
                    pieces[i].Add(new Square(square));
                }
            }

            lastPlayedMove = board.lastPlayedMove;

            isInCheck = board.isInCheck;
            turnColour = board.turnColour;

        }
    }
}
