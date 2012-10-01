using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Monyker.Model
{
    public class Account : NotifyBase
    {
        string _name;
        SolidColorBrush _colour;
        List<Transaction> _transactions;

        public string            Name         { get { return _name;         } set { _name         = value; NotifyPropertyChanged("Name"        ); } }
        public SolidColorBrush   Colour       { get { return _colour;       } set { _colour       = value; NotifyPropertyChanged("Colour"      ); } }
        public List<Transaction> Transactions { get { return _transactions; } set { _transactions = value; NotifyPropertyChanged("Transactions"); } }
    }
}
