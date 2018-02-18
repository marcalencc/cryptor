using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Cryptor.Model;

namespace Cryptor.Converters
{
    public class AlertCheckConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[3] == null)
            {
                return false;
            }

            CurrencyAlertData alertData = values[3] as CurrencyAlertData;

            if (values[0] != null && values[1] != null && values[2] != null)
            {
                double lowerBound = (double)values[0];
                double upperBound = (double)values[2];
                if (lowerBound == 0 && upperBound == 0)
                {
                    alertData.IsAlerted = false;
                    return false;
                }

                double price = (double)values[1];
                if(lowerBound > 0 && price <= lowerBound)
                {
                    alertData.IsAlerted = true;
                    return true;
                }

                if (upperBound > 0 && price >= upperBound)
                {
                    alertData.IsAlerted = true;
                    return true;
                }
            }

            alertData.IsAlerted = false;
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
