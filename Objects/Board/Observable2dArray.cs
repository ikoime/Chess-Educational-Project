using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Educational_Project
{
    /// <summary>
    /// A wrapper class for 2d ObservableCollections which make them easier to handle in code. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Observable2dArray<T>
    {
        //The data.
        public ObservableCollection<ObservableCollection<T>> data = new ObservableCollection<ObservableCollection<T>>();
        public int xSize { get; private set; }
        public int ySize { get; private set; }

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="x">The maximum xSize.</param>
        /// <param name="y">The maximum ySize.</param>
        public Observable2dArray(int x, int y)
        {
            this.xSize = x;
            this.ySize = y;
            for (int i = 0; i < xSize; i++)
            {
                data.Add(new ObservableCollection<T>());
                for (int j = 0; j < ySize; j++)
                {
                    //Adding elements to our enumerable to simulate array indexing. 
                    data[i].Add(default);
                }
            }
        }
        /// <summary>
        /// The indexer for the data inside the class.
        /// </summary>
        /// <param name="y">The y index of the data to be accessed or changed.</param>
        /// <param name="x">The x index of the data to be accessed or changed</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">Is thrown when the any of the indexers are < 0, or >= the max size for that respective dimension</exception>
        public T this[int y, int x]
        {
            get
            {
                
                if (y >= 0 && y < ySize && x >= 0 && x < xSize)
                {
                    //If the index is inside the bounds, we return the value.
                    return data[y][x];
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
                    data[y][x] = value;
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
