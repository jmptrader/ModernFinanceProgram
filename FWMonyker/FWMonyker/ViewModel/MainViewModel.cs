using FWMonyker.Command;
using FWMonyker.Model;
using FWMonyker.XML;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace FWMonyker.ViewModel
{
    public class MainViewModel : ViewModelBase, IDropTarget, INotifyPropertyChanged
    {
        public ObservableCollection<Account> Accounts { get; set; }
        public ObservableCollection<Transaction> _kontoHandlinger;
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
                _TransactionListModel.NotifyAccountChange();
                _ChartModel.NotifyAccountChange();
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

        public ICommand ChartUserControlCommand { get; private set; }

        private void ExecuteTransactionListUserControlCommand()
        {
            CurrentViewModel = _TransactionListModel;
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

            CurrentViewModel = _TransactionListModel;

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

            

            //ObservableCollection<Account> schools = new ObservableCollection<Account>();

            //foreach (var item in Accounts)
            //{
            //    schools.Add(new Account());
            //}

            //samlingAfAccounts = CollectionViewSource.GetDefaultView(Accounts);

            //KontoHandlinger = new ObservableCollection<Transaction>();

            //int i = 0;

            //foreach (var item in Accounts[i].Transactions)
            //{
            //    KontoHandlinger.Add(new Transaction());

            //    if (Accounts.Count != null)
            //        i++;
            //}
            //Debug.WriteLine(i.ToString());
            //Debug.WriteLine(KontoHandlinger.Count.ToString());
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