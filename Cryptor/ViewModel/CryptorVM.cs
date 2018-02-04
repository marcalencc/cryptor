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
        private const int MAX_MONITOR_COUNT = 5;
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
                    Symbol = "BTC"
                },
                new Currency()
                {
                    Id = "etherium",
                    Name = "Etherium",
                    Symbol ="ETH"
                },
                new Currency()
                {
                    Id = "ripple",
                    Name = "Ripple",
                    Symbol ="XRP"
                },
                new Currency()
                {
                    Id = "sia",
                    Name = "Siacoin",
                    Symbol ="SIA"
                },
                new Currency()
                {
                    Id = "funfair",
                    Name = "FunFair",
                    Symbol ="FUN"
                },
                new Currency()
                {
                    Id = "cardano",
                    Name = "Cardano",
                    Symbol ="ADA"
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

        private RelayCommand m_addRemoveMonitoredCurrency;
        public ICommand AddRemoveMonitoredCurrency
        {
            get
            {
                if (m_addRemoveMonitoredCurrency == null)
                {
                    m_addRemoveMonitoredCurrency = new RelayCommand(OnAddRemoveMonitoredCurrency);
                }
                return m_addRemoveMonitoredCurrency;
            }
        }

        private void OnAddRemoveMonitoredCurrency(object param)
        {
            Currency currency = param as Currency;
            if(currency != null)
            {
                if (MonitoredCurrenciesCount >= MAX_MONITOR_COUNT && !currency.IsMonitored)
                {
                    return;
                }

                currency.IsMonitored = !currency.IsMonitored;
                NotifyPropertyChanged("MonitoredCurrenciesCount");
                NotifyPropertyChanged("MonitoredCurrencies");
            }
        }

        private RelayCommand m_stopMonitoring;
        public ICommand StopMonitoring
        {
            get
            {
                if (m_stopMonitoring == null)
                {
                    m_stopMonitoring = new RelayCommand(OnStopMonitoring);
                }
                return m_stopMonitoring;
            }
        }

        private void OnStopMonitoring(object param)
        {
            Currency currency = param as Currency;
            if(currency != null)
            {
                currency.IsMonitored = false;
                NotifyPropertyChanged("MonitoredCurrenciesCount");
                NotifyPropertyChanged("MonitoredCurrencies");
            }
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
                return new ObservableCollection<Currency>(m_currencies.Where(c => c.IsMonitored).OrderBy(c => c.StartMonitorTime));
            }
        }

        public int MonitoredCurrenciesCount
        {
            get
            {
                return m_currencies.Where(c => c.IsMonitored).ToList().Count;
            }
        }
    }
}
