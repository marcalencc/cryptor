using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Cryptor.Converters
{
    public class NumericToStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double? number = (double) values[0];
            string numberString = values[0].ToString();
            bool isFiat = values[1].ToString().Equals("USD");
            string formattedString =  number.ToString();

            if (numberString.Contains("E-"))
            {
                string[] splitVal = numberString.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (splitVal.Length == 2)
                {
                    int decimalPlaces = int.Parse(splitVal[1]);
                    string format = String.Format(@"{{0:N{0}}}", decimalPlaces);
                    formattedString = String.Format(format, number.Value);

                    if (isFiat)
                    {
                        double val = double.Parse(formattedString);
                        if (val > 1)
                        {
                            formattedString = Math.Round(val, 2).ToString();
                        }
                        else
                        {
                            formattedString = Math.Round(val, 4).ToString();
                        }
                    }
                    return formattedString;
                }
                else
                {
                    throw new FormatException(string.Format("Truncated double value has wrong format: {0}", values[0]));
                }
            }

            // Value not truncated, display as is
            if (isFiat)
            {
                if (number > 1)
                {
                    formattedString = Math.Round((double)number, 2).ToString();
                }
                else
                {
                    formattedString = Math.Round((double)number, 4).ToString();
                }
            }
            return formattedString.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            object[] values = new object[1] { value };
            return values;
        }
    }
}
