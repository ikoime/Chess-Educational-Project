using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Numerics;
using System.Windows.Shapes;

namespace Chess_Educational_Project
{
    /// <summary>
    /// Interaction logic for ChessGame.xaml
    /// </summary>
    public partial class ChessGame : Window
    {
        public int xSize = 8;
        public int ySize = 8;
        public ChessGame()
        {
            InitializeComponent();
            //SetupGrid();
            Board board = new Board();
            itc_ChessBoard.ItemsSource = board.pieces;
            
        }
        /// <summary>
        /// Runs the setup for the grid that contains the squares for the board.
        /// </summary>
        public void SetupGrid()
        {
            //Adds all of the rows and columns that are needed.
            for (int i = 0; i < xSize; i++)
            {
                //grd_Squares.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < ySize; i++)
            {
                //grd_Squares.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                   
                    //Gets the source of the image which corresponds to that square.
                    //This returns a string from the dictionary, which is the file location of the image.
                    Dictionaries.squareColourToImage.TryGetValue((i + j) % 2 == 0 ? SquareColour.White : SquareColour.Black, out string source);
                    Image square = CreateImage(source);
                    //Adding the square to the correct position in the grid.
                    //grd_Squares.Children.Add(square);
                    Grid.SetRow(square, i);
                    Grid.SetColumn(square, j);
                    
                }
            }
        }
        public Image CreateImage(string src)
        {
            //Creating a bitmap image.
            BitmapImage temp = new BitmapImage();
            temp.BeginInit();
            //Assigning the file location to the bitmap.
            temp.UriSource = new Uri(src,UriKind.Absolute);
            temp.EndInit();
            
            Image result = new Image();
            //Assigning the bitmap as the source of the image UI element.
            result.Source = temp;
            return result;

        }
    }
}
