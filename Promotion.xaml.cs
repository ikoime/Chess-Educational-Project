using System;
using System.Collections.Generic;
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
    /// Interaction logic for Promotion.xaml
    /// </summary>
    public partial class Promotion : Window
    {
        public (Type,PieceColour) chosenType;
        public Promotion(PieceColour colour)
        {
            chosenType = (typeof(Queen), colour);
            List<(Type,PieceColour)> promotionOptions = new List<(Type,PieceColour)>()
            {
                (typeof(Queen),colour),
                (typeof(Rook), colour),
                (typeof(Knight), colour),
                (typeof(Bishop), colour)
            };  
            InitializeComponent();
            itc_Promotion.ItemsSource = promotionOptions;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            chosenType = ((Type, PieceColour))((Image)sender).DataContext;
            this.Close();
        }
    }
}
