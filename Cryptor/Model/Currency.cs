using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Cryptor.Utilities;
using System.Windows.Input;
using Newtonsoft.Json;

namespace Cryptor.Model
{
    public class Currency : IEquatable<Currency>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Currency ()
        {
            m_currencyAlertDataList = new ObservableCollection<CurrencyAlertData>();
        }

        private string m_id; // "id": "bitcoin",
        public string Id
        {
            get
            {
                return m_id;
            }
            set
            {
                if (m_id != value)
                {
                    m_id = value;
                }
            }
        }

        private string m_name; // "name": "Bitcoin",
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                if (m_name != value)
                {
                    m_name = value;
                }
            }
        }

        private string m_symbol; // "symbol": "BTC",
        public string Symbol
        {
            get
            {
                return m_symbol;
            }
            set
            {
                if (m_symbol != value)
                {
                    m_symbol = value;
                }
            }
        }

        private double? m_percentChange1Hour; // "percent_change_1h": "0.04",
        [JsonProperty("percent_change_1h")]
        public double? PercentChange1Hour
        {
            get
            {
                return m_percentChange1Hour;
            }
            set
            {
                if (m_percentChange1Hour != value)
                {
                    m_percentChange1Hour = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double? m_percentChange24Hours; //"percent_change_24h": "-0.3",
        [JsonProperty("percent_change_24h")]
        public double? PercentChange24Hours
        {
            get
            {
                return m_percentChange24Hours;
            }
            set
            {
                if (m_percentChange24Hours != value)
                {
                    m_percentChange24Hours = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double? m_percentChange7days; // "percent_change_7d": "-0.57",
        [JsonProperty("percent_change_7d")]
        public double? PercentChange7days
        {
            get
            {
                return m_percentChange7days;
            }
            set
            {
                if (m_percentChange7days != value)
                {
                    m_percentChange7days = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double? m_lastUpdated; // "last_updated": "1472762067"
        [JsonProperty("last_updated")]
        public double? LastUpdated
        {
            get
            {
                return m_lastUpdated;
            }
            set
            {
                if (m_lastUpdated != value)
                {
                    m_lastUpdated = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double? m_priceUsd;
        [JsonProperty("price_usd")]
        public double? PriceUsd
        {
            get
            {
                return m_priceUsd;
            }
            set
            {
                if(m_priceUsd != value)
                {
                    m_priceUsd = value;
                    if (m_isMonitored)
                    {
                        SetCurrencyAlertValue("USD", value);
                    }
                    NotifyPropertyChanged();
                }
            }
        }

        private double? m_priceBtc;
        [JsonProperty("price_btc")]
        public double? PriceBtc
        {
            get
            {
                return m_priceBtc;
            }
            set
            {
                if (m_priceBtc != value)
                {
                    m_priceBtc = value;
                    if (m_isMonitored)
                    {
                        SetCurrencyAlertValue("BTC", value);
                    }
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<CurrencyAlertData> m_currencyAlertDataList;
        public ObservableCollection<CurrencyAlertData> CurrencyAlertDataList
        {
            get
            {
                return m_currencyAlertDataList;
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
                    if (m_isMonitored)
                    {
                        m_startMonitorTime = DateTime.Now;
                        SetCurrencyAlertValue("USD", m_priceUsd);
                        SetCurrencyAlertValue("BTC", m_priceBtc);
                    }
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime m_startMonitorTime;
        public DateTime StartMonitorTime
        {
            get
            {
                return m_startMonitorTime;
            }
        }

        private void SetCurrencyAlertValue(string currencyName, double? value)
        {
            CurrencyAlertData alertData = m_currencyAlertDataList.FirstOrDefault(ad => ad.PairedCurrency == currencyName);
            if (alertData != null)
            {
                alertData.Price = value;
            }
            else
            {
                m_currencyAlertDataList.Add(new CurrencyAlertData(currencyName, value));
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
