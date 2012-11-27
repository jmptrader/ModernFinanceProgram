using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Media;

namespace FWMonyker.Model
{
    public class Account : NotifyBase
    {
        string _name;
        decimal _balance;
        SolidColorBrush _colour;
        IEnumerable<Transaction> _transactions;
        List<KeyValuePair<string, decimal>> _chartValueList;

        public Account()
        {
            _transactions = new List<Transaction>();
        }

        public Account This
        {
            get
            {
                return this;
            }
        }

        public string Name
        { 
            get 
            { 
                return _name;
            }
            set
            { 
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public decimal Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
                NotifyPropertyChanged("Balance");
            }
        }

        public SolidColorBrush Colour
        {
            get
            {
                return _colour;
            }
            set
            {
                _colour = value;
                NotifyPropertyChanged("Colour");
            }
        }
        public IEnumerable<Transaction> Transactions
        {
            get
            {
                return _transactions;
            }
            set
            {
                _transactions = value;
                NotifyPropertyChanged("Transactions");
            }
        }
        
        public List<KeyValuePair<string, decimal>> ChartValueList
        {
            get { return _chartValueList; }
            set
            {
                _chartValueList = value;
                NotifyPropertyChanged("ChartValueList");
            }
        }
    }
}
