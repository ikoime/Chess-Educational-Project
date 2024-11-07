using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Media;
using System.Windows.Media;
using System.ComponentModel;

namespace Chess_Educational_Project
{
    /// <summary>
    /// Converter which is bound to the square rectangle on the board. Converts the position into a colour.
    /// </summary>
    [ValueConversion(typeof(Vector2), typeof(SolidColorBrush))]
    public class SquareConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Vector2 pos = (Vector2)value;
            //Gets the value of the light and dark squares respectively from the dictionaries. This means I can change the square colour in one place.
            Dictionaries.squareColourToColour.TryGetValue((SquareColour)((pos.X + pos.Y) % 2), out SolidColorBrush colour);
            return colour;
            //return (pos.X + pos.Y) % 2 == 0 ? Brushes.Red : Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
