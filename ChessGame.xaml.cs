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
        public DispatcherTimer clockTimer;
        public ChessGame(Board board)
        {
            InitializeComponent();
            //SetupGrid();
            this.board = board;
            itc_ChessBoard.ItemsSource = board.pieces;

            //Adding clocks
            txt_ClockWhite.DataContext = board;
            txt_ClockBlack.DataContext = board;

            //Creating the timer which will reduce the turn player's time remaining.
            //Ran on Loaded (6) priority to ensure that the time is always decreasing regardless of what's going on in the main program.
            clockTimer = new DispatcherTimer(DispatcherPriority.Loaded);
            //Assigning the ClockDecrement procedure to the Tick event.
            clockTimer.Tick += ClockDecrement;
            //Making it so the procedure runs every 100 milliseconds, since I decrease the timer by 100 milliseconds every time it's ran.
            clockTimer.Interval = TimeSpan.FromMilliseconds(100);
            clockTimer.Start();
            
        }
        /// <summary>
        /// A procedure that decrements the clocks on the tick event of a dispatcher timer. Should never be ran outside of this.
        /// </summary>
        /// <param name="sender">The DispatcherTimer which invoked the Tick event.</param>
        /// <param name="e">Information about the event.</param>
        private void ClockDecrement(object? sender, EventArgs e)
        {
            //Decreasing the time for the correct colour.
            if (board.turnColour == PieceColour.White)
            {
                board.whiteTime -= TimeSpan.FromMilliseconds(100);
            }
            else
            {
                board.blackTime -= TimeSpan.FromMilliseconds(100);
            }

            //If either player has lost the game, then we display an appropriate message and close the window.
            if (board.whiteTime == TimeSpan.Zero)
            {
                clockTimer.Stop();
                MessageBox.Show("White has lost on time");
                Close();
            }
            else if (board.blackTime == TimeSpan.Zero)
            {
                clockTimer.Stop();
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
            //If they are clicking a piece whose colour does not match whose turn it is, then they cannot move it.
            if (clickedPiece.colour != board.turnColour)
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
                    //If the move is illegal, then we don't add it.
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
                    board[move.endPosition].targetMove = null;
                }
            }
            
        }

        private void Move_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Getting the information from the sender which triggered the event (in this case a legal move indicator).
            Image clickedMove = sender as Image;
            Square square = clickedMove.DataContext as Square;

            //Recording that this piece has moved (important for castling).
            board[square.targetMove.startPosition].piece.hasMoved = true;

            if (board.lastPlayedMove.moveType != MoveType.Invalid)
            {
                board[board.lastPlayedMove.endPosition].isSelected = false;
            }
            //Makes the move on the board.
            board.Move(square.targetMove,false);

            //Gets rid of the legal move indicators.
            ResetMoves();

            //Alternates the turn order.
            board.AlternateTurnOrder();

            //Records if the player is in check for that turn.
            board.isInCheck = board.PlayerInCheck(board.turnColour);

            //Gets the number of legal moves of all of the pieces on the board. If this number is 0, then its either checkmate or stalemate.
            //This can then be differentiated depending on whether or not that player is in check.
            int numMoves = (board.pieces.SelectMany(x => x).Where(x => x.piece is not null && x.piece.colour == board.turnColour)).SelectMany(x => x.piece.GetMoves(board, x.pos).Where(x => CheckLegality(x))).Count();
            if (numMoves == 0)
            {
                clockTimer.Stop();
                //If they have no legal moves, and are in check, they are checkmated.
                if (board.isInCheck)
                {
                    MessageBox.Show("Checkmate","Game over",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                }
                //Else they are stalemated.
                else
                {
                    MessageBox.Show("Stalemate");
                }
                Close();
            }
            
            square.isSelected = true;
        }
        /// <summary>
        /// Checks if a move would place or leave the turn player in check after it was hypothetically played.
        /// </summary>
        /// <param name="move">The move to be checked.</param>
        /// <returns>True if the move is legal, false else.</returns>
        private bool CheckLegality(Move move)
        {
            //Creating a temporary copy of the board.
            Board tempBoard = new Board(board);
            tempBoard.Move(move, true);
            return !tempBoard.PlayerInCheck(board.turnColour);
        }
    }
}
