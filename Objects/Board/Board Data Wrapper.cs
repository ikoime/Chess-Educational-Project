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
        /// <summary>
        /// The indexer for the data inside the class.
        /// </summary>
        /// <param name="y">The y index of the data to be accessed or changed.</param>
        /// <param name="x">The x index of the data to be accessed or changed</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">Is thrown when the any of the indexers are < 0, or >= the max size for that respective dimension</exception>
        public Square this[int y, int x]
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
        public Square this[Vector2 pos]
        {
            get
            {
                if (pos.Y >= 0 && pos.Y < ySize && pos.X >= 0 && pos.X < xSize)
                {
                    //If the index is inside the bounds, we return the value.
                    return pieces[(int)pos.Y][(int)pos.X];
                }
                else
                {
                    //Else we throw an appropriate exception.
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (pos.Y >= 0 && pos.Y < ySize && pos.X >= 0 && pos.X < xSize)
                {
                    //If the index is inside the bounds, we allow them to change the value.
                    pieces[(int)pos.Y][(int)pos.X] = value;
                }
                else
                {
                    //Else we throw an appropriate exception.
                    throw new IndexOutOfRangeException();
                }
            }
        }
    }
}
