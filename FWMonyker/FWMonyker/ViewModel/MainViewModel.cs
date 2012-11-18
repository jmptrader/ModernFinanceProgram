using FWMonyker.Model;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using FWMonyker.Command;
using System.ComponentModel;
using System.Collections.Generic;

namespace FWMonyker.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Account> Accounts { get; set; }
        public ObservableCollection<Transaction> Transactions { get; set; }
        Account _currentAccount;
        public Account CurrentAccount
        {
            get
            {
                return _currentAccount;
            }
            set
            {
                _currentAccount = value;
                Transactions.Clear();
                foreach (Transaction item in CurrentAccount.Transactions)
                {
                    Transactions.Add(item);
                }
                NotifyPropertyChanged("CurrentAccount");
            }
        }

        ICommand _selectAccount;
        public ICommand SelectAccount { get; set; }

        public void SwitchAccount(object parameter)
        {
            _selectAccount.Execute(parameter);
                
        }
        public MainViewModel()
        {
            _selectAccount = new SelectAccount(this);
            SelectAccount = new RelayCommand<object>((parameter) => SwitchAccount(parameter));
            Transactions = new ObservableCollection<Transaction>();

            Accounts = new ObservableCollection<Account>() {
                new Account() { Name = "Kristian",    Colour = new SolidColorBrush(Colors.CadetBlue)},
                new Account() { Name = "Darth Vader", Colour = new SolidColorBrush(Colors.Maroon)}
            };
            Console.WriteLine(Accounts.ToString());
            Accounts[0].Transactions = new List<Transaction>() {
                new Transaction() { Account = Accounts[0] , Description = "noget", Amount = 1000, 
                    Recipient = "nogle", TimeStamp = DateTime.Now},
                new Transaction() { Account = Accounts[0] , Description = "noget1", Amount = 1001, 
                    Recipient = "nogle1", TimeStamp = DateTime.Now},             
            };
            Accounts[1].Transactions = new List<Transaction>() {
                new Transaction() { Account = Accounts[1] , Description = "asds", Amount = 66, 
                    Recipient = "blah", TimeStamp = DateTime.Now},
                new Transaction() { Account = Accounts[1] , Description = "aasd", Amount = 42, 
                    Recipient = "Baaalh", TimeStamp = DateTime.Now},
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}