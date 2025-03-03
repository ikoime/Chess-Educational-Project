using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for FENImporting.xaml
    /// </summary>
    public partial class FENImporting : Window
    {
        public FENImporting()
        {
            InitializeComponent();
        }

        /// <summary>
        /// An event handler triggered when the button to start the game is clicked.
        /// </summary>
        /// <param name="sender">The button whose event was triggered.</param>
        /// <param name="e">The accompanying event information.</param>
        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
             
            Board board;
            if (txt_Fen.Text.Trim() == "")
            {
                //If there is no provided FEN, I create a standard position.
                board = new Board();
            }
            else
            {
                //Creating an instance of the board class with the given FEN.
                board = new Board(txt_Fen.Text);
            }
            


            //Creating an instance of the ChessGame window and displaying it to the user.
            ChessGame game = new ChessGame(board);
            game.Show();
            this.Close();
        }

       
    }
}
