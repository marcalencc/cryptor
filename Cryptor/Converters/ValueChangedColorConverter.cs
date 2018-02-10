using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Cryptor.Converters
{
    public class ValueChangedColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double currentPrice = (double) values[0];
            double previousPrice = (double) values[1];
            if (currentPrice > previousPrice)
            {
                return true;
            }
            else if (currentPrice < previousPrice)
            {
                return false;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
