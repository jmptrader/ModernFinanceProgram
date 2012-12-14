using FWMonyker.Model;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using FWMonyker.Command;
using FWMonyker.XML;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using GongSolutions.Wpf.DragDrop;
using System.Windows.Data;
using System.Collections;

namespace FWMonyker.ViewModel
{
    public class MainViewModel : ViewModelBase, IDropTarget
    {
        public ObservableCollection<Account> Accounts { get; set; }
        
        Account _currentAccount;
        List<KeyValuePair<string, decimal>> _chartValueList;
        public ObservableCollection<Transaction> _kontoHandlinger { get; set; }


        public ObservableCollection<Transaction> KontoHandlinger
        {
            get
            {
                return _kontoHandlinger;
            }
            set
            {
                _kontoHandlinger = value;
                NotifyPropertyChanged("KontoHandlinger");
            }
        }


        public List<KeyValuePair<string, decimal>> ChartValueList
        {
            get
            {
                return _chartValueList;
            }
            set
            {
                _chartValueList = value;
                NotifyPropertyChanged("ChartValueList");
            }

        }

        public Account CurrentAccount
        {
            get
            {
                return _currentAccount;
            }
            set
            {
                _currentAccount = value;
                _TransactionListModel.Transactions.Clear();
                foreach (Transaction item in CurrentAccount.Transactions)
                {
                    _TransactionListModel.Transactions.Add(item);
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

        ICommand _save;
        public ICommand Save { get; set; }

        public void SaveAccounts(object parameter)
        {
            _selectAccount.Execute(parameter);
        }


        private ViewModelBase _currentViewModel;
        public EditTransactionModel _EditTransactionModel;
        public ChartUserControlModel _ChartModel;
        public TransactionListModel _TransactionListModel;

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
                RaisePropertyChanged("CurrentViewModel");
            }
        }


        public ICommand EditTransactionUserControlerCommand { get; private set; }

        public ICommand TransactionListUserControlCommand { get; private set; }

        public ICommand ChartUserControlCommand { get; private set; }

        private void ExecuteEditTransactionUserControlerCommand()
        {
            CurrentViewModel = _TransactionListModel;
        }

        private void ExecuteTransactionListUserControlCommand()
        {
            CurrentViewModel = _EditTransactionModel;
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
            Save = new RelayCommand<object>((parameter) => SaveAccounts(parameter));
            ChartValueList = new List<KeyValuePair<string, decimal>>();

            _EditTransactionModel = new EditTransactionModel();
            _ChartModel = new ChartUserControlModel();
            _TransactionListModel = new TransactionListModel(this);

            CurrentViewModel = _TransactionListModel;
            EditTransactionUserControlerCommand = new RelayCommand(() => ExecuteEditTransactionUserControlerCommand());
            TransactionListUserControlCommand = new RelayCommand(() => ExecuteTransactionListUserControlCommand());
            ChartUserControlCommand = new RelayCommand(() => ExecuteChartUserControlCommand());

            

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
                    new Account() { Name = "Kristian",    Colour = new SolidColorBrush(Colors.CadetBlue)},
                    new Account() { Name = "Darth Vader", Colour = new SolidColorBrush(Colors.Maroon)}
                };
                Accounts[0].Transactions = new List<Transaction>() {
                    new Transaction() { Account = Accounts[0] , Description = "noget", Amount = 1000, 
                        Recipient = "nogle", TimeStamp = DateTime.Now.AddMilliseconds(2)},
                    new Transaction() { Account = Accounts[0] , Description = "noget1", Amount = 1001, 
                        Recipient = "nogle1", TimeStamp = DateTime.Now.AddHours(2)},             
                };
                Accounts[1].Transactions = new List<Transaction>() {
                    new Transaction() { Account = Accounts[1] , Description = "asds", Amount = 66, 
                        Recipient = "blah", TimeStamp = DateTime.Now.AddHours(5)},
                    new Transaction() { Account = Accounts[1] , Description = "aasd", Amount = 42, 
                        Recipient = "Baaalh", TimeStamp = DateTime.Now.AddDays(5)},
                };
            }

            CurrentAccount = Accounts[1];
            xml.SaveAccounts(Accounts);

            foreach (var item in CurrentAccount.Transactions)
            {
                ChartValueList.Add(new KeyValuePair<string, decimal>(item.Description, item.Amount));

            }
            ObservableCollection<Account> schools = new ObservableCollection<Account>();

            foreach (var item in Accounts)
            {
                schools.Add(new Account());
            }

            samlingAfAccounts = CollectionViewSource.GetDefaultView(Accounts);

            KontoHandlinger = new ObservableCollection<Transaction>();

            int i = 0;

            foreach (var item in Accounts[i].Transactions)
            {
                KontoHandlinger.Add(new Transaction());

                if (Accounts.Count != null)
                    i++;
            }
            Debug.WriteLine(i.ToString());
            Debug.WriteLine(KontoHandlinger.Count.ToString());
        }

        void IDropTarget.DragOver(DropInfo dropInfo)
        {

            if (dropInfo.Data is Transaction && dropInfo.TargetItem is Account)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        void IDropTarget.Drop(DropInfo dropInfo)
        {
            Account konto = (Account)dropInfo.TargetItem;
            Transaction kontoHandling = (Transaction)dropInfo.Data;
            konto.KontoHandlinger.Add(kontoHandling);
            ((IList)dropInfo.DragInfo.SourceCollection).Remove(kontoHandling);
        }

        public ICollectionView samlingAfAccounts { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}