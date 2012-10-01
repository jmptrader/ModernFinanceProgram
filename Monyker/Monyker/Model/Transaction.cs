using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monyker.Model
{
    public class Transaction : NotifyBase
    {
        private Account _account;
        private string _description;
        private string _recipient;
        private decimal _amount;
        private DateTime _timestamp;

        public Account  Account     { get { return _account;     } set { _account     = value; NotifyPropertyChanged("Account"    ); } }
        public string   Description { get { return _description; } set { _description = value; NotifyPropertyChanged("Description"); } }
        public string   Recipient   { get { return _recipient;   } set { _recipient   = value; NotifyPropertyChanged("Recipient"  ); } }
        public decimal  Amount      { get { return _amount;      } set { _amount      = value; NotifyPropertyChanged("Amount"     ); } }
        public DateTime TimeStamp   { get { return _timestamp;   } set { _timestamp   = value; NotifyPropertyChanged("Timestamp"  ); } }
    }
}
