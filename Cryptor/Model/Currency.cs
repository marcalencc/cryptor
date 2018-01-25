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
    public class Currency : IEquatable<Currency>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Currency ()
        {
            m_priceUsdLowerBound = 24;
            m_priceUsdUpperBound = 0.9;
            m_priceBtcLowerBound = 24.00000005;
            m_priceBtcUpperBound = 0.00000009;
        }
            
        public string Id { get; set; } // "id": "bitcoin",
        public string Name { get; set; } // "name": "Bitcoin",
        public string Symbol { get; set; } // "symbol": "BTC",
        public double PriceUsd { get; set; } // "price_usd": "573.137",
        public double PriceBtc { get; set; } // "price_btc": "1.0",
        public double PercentChange1Hour { get; set; } // "percent_change_1h": "0.04",
        public double PercentChange24Hours { get; set; } //"percent_change_24h": "-0.3",
        public double PercentChange7days { get; set; } // "percent_change_7d": "-0.57",
        public double LastUpdated { get; set; } // "last_updated": "1472762067"


        private double m_priceUsdLowerBound;
        public double PriceUsdLowerBound
        {
            get
            {
                return m_priceUsdLowerBound;
            }
            set
            {
                if (m_priceUsdLowerBound != value)
                {
                    m_priceUsdLowerBound = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double m_priceUsdUpperBound;
        public double PriceUsdUpperBound
        {
            get
            {
                return m_priceUsdUpperBound;
            }
            set
            {
                if (m_priceUsdUpperBound != value)
                {
                    m_priceUsdUpperBound = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double m_priceBtcLowerBound;
        public double PriceBtcLowerBound
        {
            get
            {
                return m_priceBtcLowerBound;
            }
            set
            {
                if (m_priceBtcLowerBound != value)
                {
                    m_priceBtcLowerBound = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double m_priceBtcUpperBound;
        public double PriceBtcUpperBound
        {
            get
            {
                return m_priceBtcUpperBound;
            }
            set
            {
                if (m_priceBtcUpperBound != value)
                {
                    m_priceBtcUpperBound = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool m_isMonitored;
        public bool IsMonitored
        {
            get
            {
                return m_isMonitored;
            }
            set
            {
                if (m_isMonitored != value)
                {
                    m_isMonitored = value;
                    NotifyPropertyChanged();
                }
            }
        }

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
