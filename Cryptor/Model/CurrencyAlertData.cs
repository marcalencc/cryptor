using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Cryptor.Model
{
    public class CurrencyAlertData : IEquatable<CurrencyAlertData>, INotifyPropertyChanged, IEditableObject
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CurrencyAlertData(string pairedCurrently) : this(pairedCurrently, 0)
        {
        }

        public CurrencyAlertData(string pairedCurrency, double? price)
        {
            m_pairedCurrency = pairedCurrency;
            m_previousPrice = price;
            m_price = price;
        }

        private string m_pairedCurrency;
        public string PairedCurrency
        {
            get
            {
                return m_pairedCurrency;
            }
        }

        private double? m_previousPrice;
        public double? PreviousPrice
        {
            get
            {
                return m_previousPrice;
            }
            set
            {
                if (m_previousPrice != value)
                {
                    m_previousPrice = value;
                }
            }
        }

        private double? m_price;
        public double? Price
        {
            get
            {
                return m_price;
            }
            set
            {
                if (m_price != value)
                {
                    m_previousPrice = m_price;
                    m_price = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double m_lowerBound;
        public double LowerBound
        {
            get
            {
                return m_lowerBound;
            }
            set
            {
                if (m_lowerBound != value)
                {
                    m_lowerBound = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double m_upperBound;
        public double UpperBound
        {
            get
            {
                return m_upperBound;
            }
            set
            {
                if (m_upperBound != value)
                {
                    m_upperBound = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double m_backUpUpperBound;
        private double m_backUpLowerBound;
        private bool m_transacting;

        public void BeginEdit()
        {
            if (!m_transacting)
            {
                m_backUpUpperBound = m_upperBound;
                m_backUpLowerBound = m_lowerBound;
                m_transacting = true;
            }
        }

        public void CancelEdit()
        {
            if (m_transacting)
            {
                m_upperBound = m_backUpUpperBound;
                m_lowerBound = m_backUpLowerBound;
                m_transacting = false;
            }
        }

        public void EndEdit()
        {
            if (m_transacting)
            {
                m_backUpUpperBound = 0;
                m_backUpLowerBound = 0;
                m_transacting = false;
            }
        }

        public bool Equals(CurrencyAlertData other)
        {
            if(other == null)
            {
                return false;
            }

            return m_pairedCurrency == other.m_pairedCurrency;
        }
    }
}
