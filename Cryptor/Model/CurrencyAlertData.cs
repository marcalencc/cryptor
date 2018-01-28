using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Cryptor.Model
{
    public class CurrencyAlertData : IEquatable<CurrencyAlertData>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public CurrencyAlertData(string pairedCurrently) : this(pairedCurrently, 0)
        {
        }

        public CurrencyAlertData(string pairedCurrently, double price)
        {
            m_pairedCurrency = pairedCurrently;
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

        private double m_price;
        public double Price
        {
            get
            {
                return m_price;
            }
            set
            {
                if (m_price != value)
                {
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
