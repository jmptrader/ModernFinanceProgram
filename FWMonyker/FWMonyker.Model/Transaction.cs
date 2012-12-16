using SharpFellows.Toolkit.Behaviours;
using System;
using System.Windows;

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
        private IDragSource _source;

        public Account Account { get { return _account; } set { _account = value; NotifyPropertyChanged("Account"); } }

        public string Description { get { return _description; } set { _description = value; NotifyPropertyChanged("Description"); } }

        public string Recipient { get { return _recipient; } set { _recipient = value; NotifyPropertyChanged("Recipient"); } }

        public decimal Amount { get { return _amount; } set { _amount = value; NotifyPropertyChanged("Amount"); } }

        public decimal BalanceAtTimeStamp { get { return _balanceAtTimeStamp; } set { _balanceAtTimeStamp = value; NotifyPropertyChanged("BalanceAtTimeStamp"); } }

        public DateTime TimeStamp { get { return _timestamp; } set { _timestamp = value; NotifyPropertyChanged("Timestamp"); } }

        public IDragSource Source
        {
            get
            {
                if (_source == null)
                {
                    _source = new DragSource<Transaction>(GetDragEffects, GetData);
                }
                return _source;
            }
        }

        private object GetData(Transaction transaction)
        {
            return this;
        }

        private DragDropEffects GetDragEffects(Transaction transaction)
        {
            return DragDropEffects.Move;
        }

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