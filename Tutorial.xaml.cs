using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chess_Educational_Project
{
    /// <summary>
    /// Interaction logic for Tutorial.xaml
    /// </summary>
    public partial class Tutorial : Window
    {
        /// <summary>
        /// The board currently being shown for the tutorial.
        /// </summary>
        public Board currentBoard;

        public List<Move> lastLegalMoves;

        public Queue<TutorialInfo> tutorialQueue;

        public Tutorial()
        {
            InitializeComponent();
            string info;
            using (StreamReader sr = new StreamReader("Tutorial/Tutorial.json"))
            {
                info = sr.ReadToEnd();
            }

            TutorialInfo[] infoLlist = JsonConvert.DeserializeObject<TutorialInfo[]>(info);
            tutorialQueue = new Queue<TutorialInfo>(infoLlist);

            ChangeTutorial();
        }

        private void Piece_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image clickedImage = sender as Image;
            Square square = clickedImage.DataContext as Square;
            Piece clickedPiece = square.piece;

            ResetMoves();
            lastLegalMoves = clickedPiece.GetMoves(currentBoard, square.pos);

            foreach (Move move in lastLegalMoves)
            {
                currentBoard[move.endPosition].targetMove = move;
            }
        }

        public void ResetMoves()
        {
            if (lastLegalMoves is not null)
            {
                foreach (Move move in lastLegalMoves)
                {
                    currentBoard[move.endPosition].targetMove = null;
                }
            }

        }

        private void Move_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Getting the information from the sender which triggered the event (in this case a legal move indicator).
            Image clickedMove = sender as Image;
            Square square = clickedMove.DataContext as Square;

            //Recording that this piece has moved (important for castling).
            currentBoard[square.targetMove.startPosition].piece.hasMoved = true;

            //Makes the move on the board.
            currentBoard.Move(square.targetMove, false);

            //Gets rid of the legal move indicators.
            ResetMoves();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeTutorial();
        }

        public void ChangeTutorial()
        {
            if (tutorialQueue.Count == 0)
            {
                Close();
                return;
            }

            TutorialInfo nextTutorial = tutorialQueue.Dequeue();

            currentBoard = new Board(nextTutorial.fen);

            itc_ChessBoard.ItemsSource = currentBoard.pieces;
            txt_Tutorial.DataContext = nextTutorial;
        }
    }
}
