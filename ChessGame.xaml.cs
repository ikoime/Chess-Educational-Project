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
using System.Threading;

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

            //Testing
            board[4, 4].piece = new Pawn(PieceColour.White);
            board[4,4].isLegalMove = true;
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            Image image = sender as Image;
            if (image is not null && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(image,image.DataContext, DragDropEffects.Move);
            }
        }

        private void txt_ClockWhite_Drop(object sender, DragEventArgs e)
        {

        }
    }
}
