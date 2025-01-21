using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Chess_Educational_Project
{
    [ValueConversion(typeof(Move),typeof(BitmapImage))]
    public class ValidMoveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Move move = (Move)value;
            if (move is not null)
            {
                Resources.move.TryGetValue(move.moveType, out string packUri);
                return Resources.CreateImageSource(packUri);
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
