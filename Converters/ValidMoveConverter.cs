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
    /// <summary>
    /// The conversion class that converts moves into images that represent moves.
    /// </summary>
    [ValueConversion(typeof(Move),typeof(BitmapImage))]
    public class ValidMoveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Casts the input value to a instance of the move class.
            Move move = (Move)value;
            if (move is not null)
            {
                //If the move is not null, I get the corresponding image from the dictionary.
                //This means that I can display captures and moves differently.
                Resources.move.TryGetValue(move.moveType, out string packUri);
                return Resources.CreateImageSource(packUri);
            }
            else
            {
                //If the move is null, I return an empty source so nothing is rendered.
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
