using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Chess_Educational_Project
{
    /// <summary>
    /// Converts the type of piece into an image for binding.
    /// </summary>

    [ValueConversion(typeof(Piece),typeof(BitmapImage))]
    public class PieceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Piece piece = (Piece?)value;
            if (piece is not null)
            {
                Dictionaries.pieceToImage.TryGetValue((piece.GetType(), piece.colour), out string packUri);
                BitmapImage tempImage = new BitmapImage();
                tempImage.BeginInit();
                tempImage.UriSource = new Uri(packUri,UriKind.Absolute);
                tempImage.EndInit();
                return tempImage;
            }
            return null;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
        
    }
}
