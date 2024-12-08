using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Chess_Educational_Project.Converters
{
    public class ValidMoveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isValid = (bool)value;
            if (isValid)
            {
                Resources.miscResources.TryGetValue(MiscResources.ValidMove, out string packUri);
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
