using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Cryptor.Utilities
{
    public class NumericToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double? number = (double) value;
            string numberString = value.ToString();
            if(numberString.Contains("E-"))
            {
                string[] splitVal = numberString.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (splitVal.Length == 2)
                {
                    int decimalPlaces = int.Parse(splitVal[1]);
                    string format = String.Format(@"{{0:N{0}}}", decimalPlaces);
                    string formattedString = String.Format(format, number.Value);
                    return formattedString;
                }
                else
                {
                    throw new FormatException(string.Format("Truncated double value has wrong format: {0}", value));
                }
            }

            // Value not truncated, display as is
            return number;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
