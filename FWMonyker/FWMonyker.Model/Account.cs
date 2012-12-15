using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace FWMonyker.Model
{
    public class Account : NotifyBase
    {
        private string _name;
        private decimal _balance;
        private Color _color;
        private IList<Transaction> _transactions;
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

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                NotifyPropertyChanged("Color");
                NotifyPropertyChanged("ColorBrush");
            }
        }

        public SolidColorBrush ColorBrush
        {
            get
            {
                return new SolidColorBrush(Color);
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
    }
}