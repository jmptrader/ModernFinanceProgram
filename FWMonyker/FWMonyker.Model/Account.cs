/*
 ****************************************
 *                                      *
 *   Author: Kristian Dam-Jensen        *
 *           og Michael Larsen          *
 ****************************************
 */

using SharpFellows.Toolkit.Behaviours;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace FWMonyker.Model
{
    public class Account : NotifyBase
    {
        private string _name;
        private decimal _balance;
        private Color _color;
        private IList<Transaction> _transactions;
        private IDropTarget _target;

        public Account()
        {
            _transactions = new List<Transaction>();
        }

        public void NotifyAllProperties()
        {
            NotifyPropertyChanged("Name");
            NotifyPropertyChanged("Balance");
            NotifyPropertyChanged("Color");
            NotifyPropertyChanged("ColorBrush");
            NotifyPropertyChanged("Transactions");
            NotifyPropertyChanged("UITransactions");
        }

        public IDropTarget DropTarget
        {
            get
            {
                if (_target == null)
                {
                    _target = new DropTarget<Transaction>(GetDropEffects, Drop);
                }
                return _target;
            }
        }

        private DragDropEffects GetDropEffects(Transaction transaction)
        {
            if (transaction.Account == this)
            {
                return DragDropEffects.None;
            }
            return DragDropEffects.Move;
        }

        private void Drop(Transaction transaction)
        {
            transaction.Account.Transactions.Remove(transaction);
            transaction.Account.NotifyPropertyChanged("UITransactions");
            transaction.Account = this;
            Transactions.Add(transaction);
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

        

        public void SetFilterKeyword(string keyword)
        {
            Keyword = keyword;
            NotifyPropertyChanged("UITransactions");
        }
        private string Keyword;
        public ObservableCollection<Transaction> UITransactions
        {
            get
            {
                IEnumerable<Transaction> filtered = from item in Transactions
                                                        where item.Description.ToLower().Contains(Keyword.ToLower()) || item.Recipient.ToLower().Contains(Keyword.ToLower())
                                                        select item;
                if (string.IsNullOrWhiteSpace(Keyword))
                    filtered = Transactions;
                return new ObservableCollection<Transaction>(filtered);
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
                NotifyPropertyChanged("UITransactions");
            }
        }

        public Account Clone()
        {
            return new Account()
            {
                Balance = this.Balance,
                Color = this.Color,
                Transactions = this.Transactions,
                Name = this.Name
            };
        }
    }
}