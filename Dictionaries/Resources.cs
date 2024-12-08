using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chess_Educational_Project
{
    public enum SquareColour
    {
        White,
        Black
    }
    public enum MiscResources
    {
        ValidMove
    }

    /// <summary>
    /// A class containing all of the dictionaries which need to be used for bindings or conversions. Acts as a central location to change images etc.
    /// </summary>
    public static class Resources
    {
        public static Dictionary<SquareColour, SolidColorBrush> squareColourToColour = new Dictionary<SquareColour, SolidColorBrush>()
        {
            { SquareColour.White, new SolidColorBrush(new Color(){A=255,R = 239, G = 216, B = 180 }) },
            { SquareColour.Black, new SolidColorBrush(new Color(){A=255,R = 180, G = 135, B = 98  }) }
        };
        /// <summary>
        /// A dictionary used for converting piece types and colours into images.
        /// </summary>
        public static Dictionary<(Type, PieceColour), string> pieceToImage = new Dictionary<(Type, PieceColour), string>()
        {
            { (typeof(Pawn),PieceColour.White), "pack://siteoforigin:,,,/Images/cburnett/wP.png"},
            { (typeof(Pawn),PieceColour.Black), "pack://siteoforigin:,,,/Images/cburnett/bP.png"},

            { (typeof(Knight),PieceColour.White), "pack://siteoforigin:,,,/Images/cburnett/wN.png"},
            { (typeof(Knight),PieceColour.Black), "pack://siteoforigin:,,,/Images/cburnett/bN.png"},

            { (typeof(Bishop),PieceColour.White), "pack://siteoforigin:,,,/Images/cburnett/wB.png"},
            { (typeof(Bishop),PieceColour.Black), "pack://siteoforigin:,,,/Images/cburnett/bB.png"},

            { (typeof(Rook),PieceColour.White), "pack://siteoforigin:,,,/Images/cburnett/wR.png"},
            { (typeof(Rook),PieceColour.Black), "pack://siteoforigin:,,,/Images/cburnett/bR.png"},

            { (typeof(Queen),PieceColour.White), "pack://siteoforigin:,,,/Images/cburnett/wQ.png"},
            { (typeof(Queen),PieceColour.Black), "pack://siteoforigin:,,,/Images/cburnett/bQ.png"},

            { (typeof(King), PieceColour.White), "pack://siteoforigin:,,,/Images/cburnett/wK.png"},
            { (typeof(King),PieceColour.Black), "pack://siteoforigin:,,,/Images/cburnett/bK.png"}
        };
        /// <summary>
        /// A dictionary used for the image packUris for any resources which dont fit into any of the other dictionaries.
        /// </summary>
        public static Dictionary<MiscResources, string> miscResources = new Dictionary<MiscResources, string>()
        {
            { MiscResources.ValidMove,"pack://siteoforigin:,,,/Images/move.png" }
        };

        public static BitmapImage CreateImageSource(string source)
        {
            BitmapImage tempImage = new BitmapImage();
            tempImage.BeginInit();
            tempImage.UriSource = new Uri(source, UriKind.Absolute);
            tempImage.EndInit();
            return tempImage;
        }
    }
}
