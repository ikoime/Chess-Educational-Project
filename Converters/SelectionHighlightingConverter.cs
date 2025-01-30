using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chess_Educational_Project
{
    /// <summary>
    /// Converter which is bound to the square rectangle on the board. Converts the isSelected property to an visibility property.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class SelectionHighlightingConverter : IValueConverter
    {
        
        

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSelected = (bool)value;
            if (isSelected)
            {
                return Visibility.Visible;
            }
            return Visibility.Hidden;
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
        
    }
}
