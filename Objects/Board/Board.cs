using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Chess_Educational_Project
{
    /// <summary>
    /// The class containing the board and their appropriate helper functions.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// The data structure holding all of the data.
        /// </summary>
        public ObservableCollection<ObservableCollection<Piece>> pieces = new ObservableCollection<ObservableCollection<Piece>>();
        /// <summary>
        /// The maximum xSize. By default, its set to 8.
        /// </summary>
        public int xSize { get; private set; } = 8;
        /// <summary>
        /// The maximum ySize. By default, its set to 8.
        /// </summary>
        public int ySize { get; private set; } = 8;
        /// <summary>
        /// The default constructor. Assumes a normal game, so creates an 8x8 board with a default starting position.
        /// </summary>
        public Board()
        {
            Setup();       
            //Setup default pieces.
        }
        /// <summary>
        /// Creates a board of a defined size.
        /// </summary>
        /// <param name="x">The maximum xSize.</param>
        /// <param name="y">The maximum ySize.</param>
        public Board(int x, int y)
        {
            this.xSize = x;
            this.ySize = y;
            Setup();
        }
        /// <summary>
        /// The indexer for the data inside the class.
        /// </summary>
        /// <param name="y">The y index of the data to be accessed or changed.</param>
        /// <param name="x">The x index of the data to be accessed or changed</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">Is thrown when the any of the indexers are < 0, or >= the max size for that respective dimension</exception>
        public Piece this[int y, int x]
        {
            get
            {

                if (y >= 0 && y < ySize && x >= 0 && x < xSize)
                {
                    //If the index is inside the bounds, we return the value.
                    return pieces[y][x];
                }
                else
                {
                    //Else we throw an appropriate exception.
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {

                if (y >= 0 && y < ySize && x >= 0 && x < xSize)
                {
                    //If the index is inside the bounds, we allow them to change the value.
                    pieces[y][x] = value;
                }
                else
                {
                    //Else we throw an appropriate exception.
                    throw new IndexOutOfRangeException();
                }

            }
        }

        private void Setup()
        {
            for (int i = 0; i < xSize; i++)
            {
                pieces.Add(new ObservableCollection<Piece>());
                for (int j = 0; j < ySize; j++)
                {
                    //Adding elements to our enumerable to simulate array indexing.
                    pieces[i].Add(default);
                }
            }
        }
    }
}
