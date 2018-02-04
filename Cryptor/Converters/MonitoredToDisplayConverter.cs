using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace Cryptor.Converters
{
    public class MonitoredToDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isMonitored = (Boolean)value;

            if (isMonitored)
            {
                if ((string)parameter == "1")
                {
                    return "#006633";
                }
                else
                {
                    return FontStyles.Normal;
                }
            }

            if ((string)parameter == "1")
            {
                return "#606060";
            }
            else
            {
                return FontStyles.Italic;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
