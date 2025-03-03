using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Chess_Educational_Project
{
    /// <summary>
    /// Interaction logic for Promotion.xaml
    /// </summary>
    public partial class Promotion : Window
    {
        /// <summary>
        /// The piece chosen to be promoted into.
        /// </summary>
        public (Type,PieceColour) chosenType;
        public Promotion(PieceColour colour)
        {
            chosenType = (typeof(Queen), colour);
            //Creating the data structure which will be the binding for the ItemsControl.
            List<(Type,PieceColour)> promotionOptions = new List<(Type,PieceColour)>()
            {
                (typeof(Queen),colour),
                (typeof(Knight), colour),
                (typeof(Rook), colour),
                (typeof(Bishop), colour)
            };  
            InitializeComponent();
            itc_Promotion.ItemsSource = promotionOptions;

            //Gets dpi information to convert the mouse cursor position into a position for the window.
            DpiScale dpi = VisualTreeHelper.GetDpi(this);
            POINT P;
            GetCursorPos(out P);
            this.Left = (P.X / dpi.DpiScaleX) - 25;
            this.Top = (P.Y / dpi.DpiScaleY) - 25;


        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Gets the data of the clicked on image, as this is all the information we need to return.
            chosenType = ((Type, PieceColour))((Image)sender).DataContext;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

        /// <summary>
        /// An imported function from the user32.dll. Gets the mouse cursor position on the screen.
        /// </summary>
        /// <param name="lpPoint">A struct representing the point the mouse cursor lies at.</param>
        /// <returns>A boolean indicating success.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpPoint);

       
    }



    public struct POINT
    {
        public int X;
        public int Y;
    }
}
