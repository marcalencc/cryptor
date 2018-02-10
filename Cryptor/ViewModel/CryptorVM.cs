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
using Cryptor.Services;
using System.Timers;

namespace Cryptor.ViewModel
{
    public class CryptorVM : INotifyPropertyChanged, IDisposable
    {
        private const int MAX_MONITOR_COUNT = 5;
        private IDataProvider m_dataProvider;
        public IDataProvider DataProvider
        {
            get
            {
                return m_dataProvider;
            }
            set
            {
                m_dataProvider = value;
            }
        }

        private bool m_disposed;
        private Timer m_requestTimer;

        private async void RequestTimerCallback(object sender, ElapsedEventArgs e)
        {
            Task<List<Currency>> getDatTask = m_dataProvider.GetData();

            // Reset Timer while waiting for request completion
            m_requestTimer.Stop();
            m_requestTimer.Interval = 30000;
            m_requestTimer.Start();

            List<Currency> data = await getDatTask;
            foreach(Currency currency in data)
            {
                await App.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(() =>
                        {
                            AddOrUpdateCurrency(currency);
                        }));
            }
        }

        private void AddOrUpdateCurrency(Currency currency)
        {
            Currency existing = m_currencies.FirstOrDefault(c => c.Equals(currency));
            if (existing == null)
            {
                m_currencies.Add(currency);
            }
            else
            {
                if(existing.LastUpdated != currency.LastUpdated)
                {
                    existing.LastUpdated = currency.LastUpdated;
                }

                if (existing.PercentChange1Hour != currency.PercentChange1Hour)
                {
                    existing.PercentChange1Hour = currency.PercentChange1Hour;
                }

                if (existing.PercentChange24Hours != currency.PercentChange24Hours)
                {
                    existing.PercentChange24Hours = currency.PercentChange24Hours;
                }

                if (existing.PercentChange7days != currency.PercentChange7days)
                {
                    existing.PercentChange7days = currency.PercentChange7days;
                }

                if (existing.PriceBtc != currency.PriceBtc)
                {
                    existing.PriceBtc = currency.PriceBtc;
                }

                if (existing.PriceUsd != currency.PriceUsd)
                {
                    existing.PriceUsd = currency.PriceUsd;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            m_dataProvider = new CoinMarketCapDataProvider();
            m_searchText = string.Empty;
            m_currencies = new ObservableCollection<Currency>();
            m_monitoredCurrencies = new ObservableCollection<Currency>();
            m_filteredCurrenciesView = CollectionViewSource.GetDefaultView(m_currencies);
            m_requestTimer = new Timer();
            m_requestTimer.AutoReset = true;
            m_requestTimer.Elapsed += RequestTimerCallback;
            m_requestTimer.Start();
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
                if(currency.IsMonitored)
                {
                    m_monitoredCurrencies.Add(currency);
                }
                else
                {
                    m_monitoredCurrencies.Remove(currency);
                }
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
                m_monitoredCurrencies.Remove(currency);
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

        private ObservableCollection<Currency> m_monitoredCurrencies;
        public ObservableCollection<Currency> MonitoredCurrencies
        {
            get
            {
                return m_monitoredCurrencies;
            }
            set
            {
                if (m_monitoredCurrencies != value)
                {
                    m_monitoredCurrencies = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int MonitoredCurrenciesCount
        {
            get
            {
                return m_monitoredCurrencies.Count;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    if (m_requestTimer != null)
                    {
                        m_requestTimer.Stop();
                        m_requestTimer.Dispose();
                        m_requestTimer = null;
                    }
                }

                m_disposed = true;
            }
        }
    }
}
