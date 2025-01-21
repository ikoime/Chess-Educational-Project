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
        public Board board;
        public List<Move> lastLegalMoves;
        public ChessGame()
        {
            InitializeComponent();
            //SetupGrid();
            board = new Board();
            itc_ChessBoard.ItemsSource = board.pieces;
        }

        

        private void Piece_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Starts by clearing all of the valid move indicators.
            ResetMoves();
            Image clickedImage = sender as Image;
            Square square = clickedImage.DataContext as Square;
            Piece clickedPiece = square.piece;
            if (clickedPiece is null)
            {
                //Should never occur.
                return;
            }
            else
            {
                //Getting all of the legal moves for that piece.
                List<Move> legalMoves = clickedPiece.GetMoves(board,square.pos);
                foreach (Move move in legalMoves)
                {
                    //Iterating through each of the moves in the legalMoves list in order to mark them on the board.
                    board[(int)move.endPosition.Y, (int)move.endPosition.X].targetMove = move;
                }
                //Save the list to a temp variable.
                lastLegalMoves = legalMoves;
            }
            

        }
        public void ResetMoves()
        {
            if (lastLegalMoves is not null)
            {
                foreach (Move move in lastLegalMoves)
                {
                    board[(int)move.endPosition.Y, (int)move.endPosition.X].targetMove = null;
                }
            }
            
        }

        private void Move_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image clickedMove = sender as Image;
            Square square = clickedMove.DataContext as Square;
            board.Move(square.targetMove);
            ResetMoves();
        }
    }
}
