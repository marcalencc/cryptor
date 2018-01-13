using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Cryptor.Utilities;
using System.Windows.Input;

namespace Cryptor.Model
{
    public class Currency : INotifyPropertyChanged, IEquatable<Currency>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Currency ()
        {
        }

        public string Id { get; set; } // "id": "bitcoin",
        public string Name { get; set; } // "name": "Bitcoin",
        public string Symbol { get; set; } // "symbol": "BTC",
        public double PriceUsd { get; set; } // "price_usd": "573.137",
        public double PriceBtc { get; set; } // "price_btc": "1.0",
        public double PercentChange1Hour { get; set; } // "percent_change_1h": "0.04",
        public double PercentChange24Hours { get; set; } //"percent_change_24h": "-0.3",
        public double PercentChange7days { get; set; } // "percent_change_7d": "-0.57",
        public DateTime LastUpdated { get; set; } // "last_updated": "1472762067"

        public bool Equals(Currency other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id;
        }
    }
}
