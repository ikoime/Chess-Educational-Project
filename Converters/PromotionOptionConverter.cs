using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Chess_Educational_Project
{
    /// <summary>
    /// Converts a (Type,PieceColour) enum into an image for a use in the promotion screen.
    /// </summary>

    [ValueConversion(typeof((Type,PieceColour)), typeof(BitmapImage))]
    public class PromotionOptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            (Type,PieceColour) type = ((Type, PieceColour))value;
            Resources.pieceToImage.TryGetValue(type, out string packUri);
            return Resources.CreateImageSource(packUri);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
