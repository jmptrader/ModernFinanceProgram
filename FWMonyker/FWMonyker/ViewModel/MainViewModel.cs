/*
 ****************************************
 *                                      *
 *   Author: Kristian Dam-Jensen        *
 *           & Michael Boe Larsen       *
 *           & Lennart Jacobsen         *
 *                                      *
 ****************************************
 */

using FWMonyker.Command;
using FWMonyker.Model;
using FWMonyker.XML;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace FWMonyker.ViewModel
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ObservableCollection<Account> Accounts { get; set; }

        private Account _currentAccount;

        public Account CurrentAccount
        {
            get
            {
                return _currentAccount;
            }
            set
            {
                _currentAccount = value;
                _ChartModel.NotifyAccountChange();
                _EditAccountModel.NotifyAccountChange();
                NotifyPropertyChanged("CurrentAccount");
            }
        }

        private ICommand _selectAccount;

        public ICommand SelectAccount { get; set; }

        public void SwitchAccount(object parameter)
        {
            _selectAccount.Execute(parameter);
        }

        private ICommand _save;

        public ICommand Save { get; set; }

        public void SaveAccounts()
        {
            _save.Execute(Accounts);
        }

        private ViewModelBase _currentViewModel;
        public EditTransactionModel _EditTransactionModel;
        public ChartUserControlModel _ChartModel;
        public TransactionListModel _TransactionListModel;
        public EditAccountModel _EditAccountModel;

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel == value)
                    return;
                _currentViewModel = value;
                NotifyPropertyChanged("CurrentViewModel");
            }
        }

        public ICommand TransactionListUserControlCommand { get; private set; }

        public ICommand EditAccountControlCommand { get; private set; }

        public ICommand ChartUserControlCommand { get; private set; }

        private void ExecuteTransactionListUserControlCommand()
        {
            CurrentViewModel = _TransactionListModel;
        }

        private void ExecuteEditAccountControlCommand(object parameter)
        {
            var account = (parameter as Account) == null ? new Account() { Balance = 0, Color = RandomColor(), Transactions = new List<Transaction>(), Name = "" } : parameter as Account;
            CurrentViewModel = _EditAccountModel;
            _EditAccountModel.EndStateAccount = account;
            _EditAccountModel.InitialStateAccount = parameter as Account;
        }

        private Color RandomColor()
        {
            Random random = new Random();
            byte r = (byte)random.Next(255);
            byte g = (byte)random.Next(255);
            byte b = (byte)random.Next(255);

            return Color.FromRgb(r, g, b);
        }

        private void ExecuteChartUserControlCommand()
        {
            CurrentViewModel = _ChartModel;
        }

        public MainViewModel()
        {
            _selectAccount = new SelectAccount(this);
            SelectAccount = new RelayCommand<object>((parameter) => SwitchAccount(parameter));

            _save = new Save(this);
            Save = new RelayCommand(SaveAccounts);

            _EditTransactionModel = new EditTransactionModel(this);
            _ChartModel = new ChartUserControlModel(this);
            _TransactionListModel = new TransactionListModel(this);
            _EditAccountModel = new EditAccountModel(this);

            CurrentViewModel = _TransactionListModel;

            TransactionListUserControlCommand = new RelayCommand(() => ExecuteTransactionListUserControlCommand());
            ChartUserControlCommand = new RelayCommand(() => ExecuteChartUserControlCommand());
            EditAccountControlCommand = new RelayCommand<object>((parameter) => ExecuteEditAccountControlCommand(parameter));

            var xml = ObjextXMLSerializer.GetInstance;
            Accounts = new ObservableCollection<Account>();
            try
            {
                Accounts = new ObservableCollection<Account>(xml.LoadAccounts());
            }
            catch (Exception)
            {
            }
            if ((Accounts == null) || Accounts.Count == 0)
            {
                Accounts = new ObservableCollection<Account>() {
                    new Account() { Name = "Main",    Color = Colors.DarkCyan},
                };
            }
            CurrentAccount = Accounts[0];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}