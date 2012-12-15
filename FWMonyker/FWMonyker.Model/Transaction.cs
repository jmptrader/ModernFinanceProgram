using System;

namespace FWMonyker.Model
{
    public class Transaction : NotifyBase
    {
        private Account _account;
        private string _description;
        private string _recipient;
        private decimal _amount;
        private DateTime _timestamp;
        private decimal _balanceAtTimeStamp;

        public Account Account { get { return _account; } set { _account = value; NotifyPropertyChanged("Account"); } }

        public string Description { get { return _description; } set { _description = value; NotifyPropertyChanged("Description"); } }

        public string Recipient { get { return _recipient; } set { _recipient = value; NotifyPropertyChanged("Recipient"); } }

        public decimal Amount { get { return _amount; } set { _amount = value; NotifyPropertyChanged("Amount"); } }

        public decimal BalanceAtTimeStamp { get { return _balanceAtTimeStamp; } set { _balanceAtTimeStamp = value; NotifyPropertyChanged("BalanceAtTimeStamp"); } }

        public DateTime TimeStamp { get { return _timestamp; } set { _timestamp = value; NotifyPropertyChanged("Timestamp"); } }

        public Transaction Clone()
        {
            return new Transaction
            {
                Account = this.Account,
                Description = this.Description,
                Recipient = this.Recipient,
                Amount = this.Amount,
                BalanceAtTimeStamp = this.BalanceAtTimeStamp,
                TimeStamp = this.TimeStamp
            };
        }
    }
}