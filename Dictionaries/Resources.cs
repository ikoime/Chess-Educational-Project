using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        /// A dictionary used for the image packUris for moves.
        /// </summary>
        public static Dictionary<MoveType, string> move = new Dictionary<MoveType, string>()
        {
            { MoveType.Default,"pack://siteoforigin:,,,/Images/move.png" },

            { MoveType.ShortCastling, "pack://siteoforigin:,,,/Images/move.png"},
            { MoveType.LongCastling, "pack://siteoforigin:,,,/Images/move.png" },

            { MoveType.Capture,"pack://siteoforigin:,,,/Images/capture.png" },

            { MoveType.EnPassant,"pack://siteoforigin:,,,/Images/move.png" },
            { MoveType.FirstPawnMove,"pack://siteoforigin:,,,/Images/move.png" },

            { MoveType.Promotion, "pack://siteoforigin:,,,/Images/move.png" },
            { MoveType.PromotionCapture,"pack://siteoforigin:,,,/Images/capture.png" }

        };

        /// <summary>
        /// A dictionary used in FEN parsing that gives the expected position for a rook to be if castling is available.
        /// </summary>
        public static Dictionary<char, Vector2> rookPositionsForCastling = new Dictionary<char, Vector2>()
        {
            {'K',new Vector2(7,7)},
            {'Q',new Vector2(0,7)},
            {'k',new Vector2(7,0)},
            {'q',new Vector2(0,0)}
        };

        /// <summary>
        /// A dictionary which converts a character into a piece object based on algebraic notation.
        /// </summary>
        public static Dictionary<char, Piece> pieceNotationToPieceObject = new Dictionary<char, Piece>()
        {
            //All of the white pieces.
            {'K', new King(PieceColour.White) },
            {'Q', new Queen(PieceColour.White) },
            {'R', new Rook(PieceColour.White) },
            {'B', new Bishop(PieceColour.White) },
            {'N', new Knight(PieceColour.White) },
            {'P', new Pawn(PieceColour.White) },

            //All of the black pieces.
            {'k', new King(PieceColour.Black) },
            {'q', new Queen(PieceColour.Black) },
            {'r', new Rook(PieceColour.Black) },
            {'b', new Bishop(PieceColour.Black) },
            {'n', new Knight(PieceColour.Black) },
            {'p', new Pawn(PieceColour.Black) },
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
