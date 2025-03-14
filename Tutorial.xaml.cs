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

        /// <summary>
        /// <inheritdoc cref="ChessGame.lastLegalMoves"/>
        /// </summary>
        public List<Move> lastLegalMoves;

        /// <summary>
        /// The data structure containing all of the tutorials.
        /// </summary>
        public Queue<TutorialInfo> tutorialQueue;

        public Tutorial()
        {
            InitializeComponent();

            //Declaring the raw string that contains the json data.
            string info;
            using (StreamReader sr = new StreamReader("Tutorial/Tutorial.json"))
            {
                //Reading through the whole file.
                //After this process the StreamReader is discarded since I don't need to read the file anymore.
                info = sr.ReadToEnd();
            }

            //Converting the raw string into a list of our tutorial objects.
            TutorialInfo[] infoList = JsonConvert.DeserializeObject<TutorialInfo[]>(info);

            //Using this IEnumerable object to declare a new Queue object.
            tutorialQueue = new Queue<TutorialInfo>(infoList);

            //Running the function to dequeue a tutorial and change the bindings.
            ChangeTutorial();
        }

        private void Piece_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Getting the piece that was clicked.
            Image clickedImage = sender as Image;
            Square square = clickedImage.DataContext as Square;
            Piece clickedPiece = square.piece;

            //Removing all of the legal move indicators from any previously clicked pieces.
            ResetMoves();
            
            //Setting the last legal moves to the legal moves of the piece that was clicked.
            lastLegalMoves = clickedPiece.GetMoves(currentBoard, square.pos);

            //Marking each of these moves on the board.
            foreach (Move move in lastLegalMoves)
            {
                currentBoard[move.endPosition].targetMove = move;
            }
        }
        /// <summary>
        /// <inheritdoc cref="ChessGame.ResetMoves()"/>
        /// </summary>
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
            //If the queue is empty, then the tutorial is done so we close the window.
            //This also avoids errors with the Dequeue function throwing an exception if the queue is empty.
            if (tutorialQueue.Count == 0)
            {
                Close();
                return;
            }

            //Getting the next tutorial from the queue.
            TutorialInfo nextTutorial = tutorialQueue.Dequeue();

            //Creating a new board object using the fen in the tutorial.
            currentBoard = new Board(nextTutorial.fen);

            //Setting the bindings. This binds the board object to the ItemsControl and the text from the tutorial to the TextBlock.
            itc_ChessBoard.ItemsSource = currentBoard.pieces;
            txt_Tutorial.DataContext = nextTutorial;
        }
    }
}
