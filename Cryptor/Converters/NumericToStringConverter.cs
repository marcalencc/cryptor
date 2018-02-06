using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Cryptor.Converters
{
    public class NumericToStringConverter
    {
        private string FormatFiat(double? number)
        {
            string formattedString;
            if (number > 1)
            {
                formattedString = Math.Round((double)number, 2).ToString();
            }
            else
            {
                formattedString = Math.Round((double)number, 4).ToString();
            }

            return formattedString;
        }

        protected string Convert(object value, object currency)
        {
            if(value == null || currency == null)
            {
                return "";
            }

            double? number = (double)value;
            string numberString = value.ToString();
            bool isFiat = currency.ToString().Equals("USD");
            string formattedString = number.ToString();

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
                    throw new FormatException(string.Format("Truncated double value has wrong format: {0}", value));
                }
            }
            else
            {
                // Value not truncated, display as is
                if (isFiat)
                {
                    return FormatFiat(number);
                }
                return formattedString.ToString();
            }
        }
    }

    public class NumericToStringConverterMultiValue : NumericToStringConverter, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter != null && parameter.ToString() == "Price")
            {
                return string.Format("{0} {1}", Convert(values[0], values[1]), values[1]);
            }
            return Convert(values[0], values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            object[] values = new object[1] { value };
            return values;
        }
    }

    public class NumericToStringConverterSingleValue : NumericToStringConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert(value, parameter);
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
