using System.Collections.Generic;
using System.

namespace FWMonyker.Model
{
    public class Account : NotifyBase
    {
        string _name;
        Color _colour;
        List<Transaction> _transactions;

        public string            Name         { get { return _name;         } set { _name         = value; NotifyPropertyChanged("Name"        ); } }
        public SolidColorBrush   Colour       { get { return _colour;       } set { _colour       = value; NotifyPropertyChanged("Colour"      ); } }
        public List<Transaction> Transactions { get { return _transactions; } set { _transactions = value; NotifyPropertyChanged("Transactions"); } }
    }
}
