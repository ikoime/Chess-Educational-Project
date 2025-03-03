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

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            Board board = new Board(txt_Fen.Text);
            
            ChessGame game = new ChessGame(board);
            game.Show();
            this.Close();
        }

       
    }
}
