using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Data;
using Cryptor.Model;
using Cryptor.Utilities;

namespace Cryptor.ViewModel
{
    public class CryptorVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICollectionView m_filteredCurrenciesView;

        private bool Contains(object param)
        {
            Currency currency = param as Currency;
            return currency.Name.Contains(m_searchText, StringComparison.OrdinalIgnoreCase) ||
                currency.Symbol.Contains(m_searchText, StringComparison.OrdinalIgnoreCase);
        }

        public CryptorVM()
        {
            m_searchText = string.Empty;
            m_currencies = new ObservableCollection<Currency>()
            {
                new Currency()
                {
                    Id = "bitcoin",
                    Name = "Bitcoin",
                    Symbol = "BTC",
                    PriceUsd = 24.5,
                    PriceBtc = 0.000548,
                    LastUpdated = DateTime.Now,
                    PercentChange1Hour = 2.3,
                    PercentChange24Hours = -9.8,
                    PercentChange7days = 0.2
                },
                new Currency()
                {
                    Id = "etherium",
                    Name = "Etherium",
                    Symbol ="ETH",
                    PriceUsd = 24.5,
                    PriceBtc = 0.000548,
                    LastUpdated = DateTime.Now,
                    PercentChange1Hour = 2.3,
                    PercentChange24Hours = -9.8,
                    PercentChange7days = 0.2
                },
                new Currency()
                {
                    Id = "ripple",
                    Name = "Ripple",
                    Symbol ="XRP",
                    PriceUsd = 24.5,
                    PriceBtc = 0.000548,
                    LastUpdated = DateTime.Now,
                    PercentChange1Hour = 2.3,
                    PercentChange24Hours = -9.8,
                    PercentChange7days = 0.2
                },
                new Currency()
                {
                    Id = "sia",
                    Name = "Siacoin",
                    Symbol ="SIA",
                    PriceUsd = 24.5,
                    PriceBtc = 0.000548,
                    LastUpdated = DateTime.Now,
                    PercentChange1Hour = 2.3,
                    PercentChange24Hours = -9.8,
                    PercentChange7days = 0.2
                },
                new Currency()
                {
                    Id = "funfair",
                    Name = "FunFair",
                    Symbol ="FUN",
                    PriceUsd = 24.5,
                    PriceBtc = 0.000548,
                    LastUpdated = DateTime.Now,
                    PercentChange1Hour = 2.3,
                    PercentChange24Hours = -9.8,
                    PercentChange7days = 0.2
                },
                new Currency()
                {
                    Id = "cardano",
                    Name = "Cardano",
                    Symbol ="ADA",
                    PriceUsd = 24.5,
                    PriceBtc = 0.000548,
                    LastUpdated = DateTime.Now,
                    PercentChange1Hour = 2.3,
                    PercentChange24Hours = -9.8,
                    PercentChange7days = 0.2
                }
            };
            m_filteredCurrenciesView = CollectionViewSource.GetDefaultView(m_currencies);
        }

        // COMMANDS
        private RelayCommand m_searchTextBoxTextChanged;
        public ICommand SearchTextBoxTextChanged
        {
            get
            {
                if (m_searchTextBoxTextChanged == null)
                {
                    m_searchTextBoxTextChanged = new RelayCommand(OnSearchTextBoxTextChanged);
                }
                return m_searchTextBoxTextChanged;
            }
        }

        private void OnSearchTextBoxTextChanged(object param)
        {
            if (string.IsNullOrEmpty(m_searchText))
            {
                m_filteredCurrenciesView.Filter = null;
            }
            else
            {
                m_filteredCurrenciesView.Filter += Contains;
            }
        }

        private RelayCommand m_addMonitoredCurrency;
        public ICommand AddMonitoredCurrency
        {
            get
            {
                if (m_addMonitoredCurrency == null)
                {
                    m_addMonitoredCurrency = new RelayCommand(OnContextMenuClicked);
                }
                return m_addMonitoredCurrency;
            }
        }

        private void OnContextMenuClicked(object param)
        {
            Currency currency = param as Currency;
            if(currency != null)
            {
                currency.IsMonitored = !currency.IsMonitored;
            }

            NotifyPropertyChanged("MonitoredCurrencies");
        }

        // OBSERVABLES
        private string m_searchText;
        public string SearchText
        {
            get
            {
                return m_searchText;
            }
            set
            {
                if (m_searchText != value)
                {
                    m_searchText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<Currency> m_currencies;
        public ObservableCollection<Currency> Currencies
        {
            get
            {
                return m_currencies;
            }
            set
            {
                if (m_currencies != value)
                {
                    m_currencies = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<Currency> MonitoredCurrencies
        {
            get
            {
                ObservableCollection <Currency> var = new ObservableCollection<Currency>(m_currencies.Where(c => c.IsMonitored));
                return var;
            }
        }
    }
}
