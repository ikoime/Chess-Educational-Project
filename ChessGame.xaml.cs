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
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.CodeDom;

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
        public Square lastSquare;
        public bool isInCheck = false;
        public PieceColour turnColour;
        public ChessGame()
        {
            InitializeComponent();
            //SetupGrid();
            board = new Board();
            itc_ChessBoard.ItemsSource = board.pieces;

            //Adding clocks
            txt_ClockWhite.DataContext = board;
            txt_ClockBlack.DataContext = board;
            DispatcherTimer clockTimer = new DispatcherTimer(DispatcherPriority.Render);
            clockTimer.Tick += ClockDecrement;
            clockTimer.Interval = TimeSpan.FromMilliseconds(100);
            clockTimer.Start();

            
        }

        private void ClockDecrement(object? sender, EventArgs e)
        {
            if (turnColour == PieceColour.White)
            {
                board.whiteTime -= TimeSpan.FromMilliseconds(100);
            }
            else
            {
                board.blackTime -= TimeSpan.FromMilliseconds(100);
            }

            if (board.whiteTime == TimeSpan.Zero)
            {
                MessageBox.Show("White has lost on time");
                Close();
            }
            if (board.blackTime == TimeSpan.Zero)
            {
                MessageBox.Show("Black has lost on time");
                Close();
            }
        }

        private void Piece_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (lastSquare is not null)
            {
                lastSquare.isSelected = false;
            }
            Image clickedImage = sender as Image;
            Square square = clickedImage.DataContext as Square;
            Piece clickedPiece = square.piece;
            if (clickedPiece.colour != turnColour)
            {
                return;
            }
            //Starts by clearing all of the valid move indicators.
            ResetMoves();
            if (square == lastSquare)
            {
                lastSquare = null;
                square.isSelected = false;
                return;
            }
            if (clickedPiece is null)
            {
                //Should never occur.
                return;
            }
            else
            {
                lastSquare = square;
                //Getting all of the legal moves for that piece.
                List<Move> legalMoves = clickedPiece.GetMoves(board,square.pos);
                foreach (Move move in legalMoves)
                {
                    
                    
                    if (!CheckLegality(move))
                    {
                        continue;
                    }
                    //Iterating through each of the moves in the legalMoves list in order to mark them on the board.
                    board[(int)move.endPosition.Y, (int)move.endPosition.X].targetMove = move;
                    
                    
                }
                square.isSelected = false;

                //Save the list to a temp variable.
                lastLegalMoves = legalMoves;
                
            }

            square.isSelected = true;

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
            board[square.targetMove.startPosition].piece.hasMoved = true;
            board.Move(square.targetMove,false);
            ResetMoves();
            
            turnColour = (PieceColour)Convert.ToInt32((!(Convert.ToBoolean(turnColour))));
            if (board.TurnPlayerInCheck(turnColour))
            {
                isInCheck = true;
            }
            else
            {
                isInCheck = false;
            }

            int numMoves = (board.pieces.SelectMany(x => x).Where(x => x.piece is not null && x.piece.colour == turnColour)).SelectMany(x => x.piece.GetMoves(board, x.pos).Where(x => CheckLegality(x))).Count();
            if (numMoves == 0 && isInCheck)
            {
                MessageBox.Show("Checkmate");
                Close();
            }
            else if(numMoves == 0)
            {
                MessageBox.Show("Stalemate");
            }
        }
        private bool CheckLegality(Move move)
        {
            Board tempBoard = new Board(board);
            tempBoard.Move(move, true);
            if (tempBoard.TurnPlayerInCheck(turnColour))
            {
                return false;
            }
            return true;
        }
    }
}
