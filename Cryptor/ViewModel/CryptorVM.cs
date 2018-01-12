using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
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

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IsSelected")
            {
                Currency currency = sender as Currency;
                if(currency != null)
                {
                    if (currency.IsSelected)
                    {
                        if (!m_selectedCurrencies.Contains(currency))
                        {
                            m_selectedCurrencies.Add(currency);
                            NotifyPropertyChanged("SelectedCurrencies");
                            m_selectedCurrenciesString = String.Join(", ", m_selectedCurrencies.Select(c => c.Name));
                            NotifyPropertyChanged("SelectedCurrenciesString");
                        }
                    }
                    else
                    {
                        m_selectedCurrencies.Remove(currency);
                        NotifyPropertyChanged("SelectedCurrencies");
                        m_selectedCurrenciesString = String.Join(", ", m_selectedCurrencies.Select(c => c.Name));
                        NotifyPropertyChanged("SelectedCurrenciesString");
                    }
                }
            }
        }

        private RelayCommand _checkUncheckCurrencyCmd;
        public ICommand CheckUncheckCurrencyCmd
        {
            get
            {
                if (_checkUncheckCurrencyCmd == null)
                {
                    _checkUncheckCurrencyCmd = new RelayCommand(CheckUncheckCurrency);
                }
                return _checkUncheckCurrencyCmd;
            }
        }

        private void CheckUncheckCurrency(object state)
        {
        }

        public CryptorVM()
        {
            m_currencies = new ObservableCollection<Currency>()
            {
                new Currency(OnModelPropertyChanged)
                {
                    Id = "bitcoin",
                    Name = "Bitcoin",
                    Symbol ="BTC"
                },
                new Currency(OnModelPropertyChanged)
                {
                    Id = "etherium",
                    Name = "Etherium",
                    Symbol ="ETH"
                },
                new Currency(OnModelPropertyChanged)
                {
                    Id = "ripple",
                    Name = "Ripple",
                    Symbol ="XRP"
                }
            };

            m_selectedCurrencies = new List<Currency>();
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

        private List<Currency> m_selectedCurrencies;
        public List<Currency> SelectedCurrencies
        {
            get
            {
                return m_selectedCurrencies;
            }
            set
            {
                if (m_selectedCurrencies != value)
                {
                    m_selectedCurrencies = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string m_selectedCurrenciesString;
        public string SelectedCurrenciesString
        {
            get
            {
                return m_selectedCurrenciesString;
            }
            set
            {
                if (m_selectedCurrenciesString != value)
                {
                    m_selectedCurrenciesString = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
