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
                    Symbol ="BTC"
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
                }
            };
            m_filteredCurrenciesView = CollectionViewSource.GetDefaultView(m_currencies);
            m_selectedCurrencies = new List<Currency>();
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
    }
}
