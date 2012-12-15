using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace FWMonyker.Model
{
    public class Account : NotifyBase
    {
        private string _name;
        private decimal _balance;
        private SolidColorBrush _colour;
        private IList<Transaction> _transactions;
        private List<KeyValuePair<string, decimal>> _chartValueList;
        private ObservableCollection<Transaction> _kontoHandlinger;

        public Account()
        {
            _transactions = new List<Transaction>();
            KontoHandlinger = new ObservableCollection<Transaction>();
        }

        public ObservableCollection<Transaction> KontoHandlinger
        {
            get { return _kontoHandlinger; }

            set
            {
                _kontoHandlinger = value;
                NotifyPropertyChanged("kontoHandlinger");
            }
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

        public IList<Transaction> Transactions
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