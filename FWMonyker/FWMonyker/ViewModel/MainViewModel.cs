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
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Account> Accounts { get; set; }
        public ObservableCollection<Transaction> Transactions { get; set; }
        Account _currentAccount;
        List<KeyValuePair<string, decimal>> _chartValueList;
        
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
                Transactions.Clear();
                foreach (Transaction item in CurrentAccount.Transactions)
                {
                    Transactions.Add(item);
                }
                NotifyPropertyChanged("CurrentAccount");
            }
        }
        bool _ucVisibility = true;
        public bool UcVisibility
        {
            get
            {
                return _ucVisibility;
            }
            set
            {
                _ucVisibility = value;
                NotifyPropertyChanged("UcVisibility");
            }
        }

        ICommand _visibilitySwitch;
        public ICommand VisibilitySwitch { get; set; }

        public void SwitchVisibility(object parameter)
        {
            _visibilitySwitch.Execute(parameter);
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

        readonly static EditTransactionModel _EditTransactionModel = new EditTransactionModel();

        readonly static TransactionListModel _TransactionListModel = new TransactionListModel();

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

        private void ExecuteEditTransactionUserControlerCommand()
        {
        //    CurrentViewModel = MainViewModel._EditTransactionModel;
        }

        /// <summary>
        /// Set the CurrentViewModel to 'SecondViewModel'
        /// </summary>
        private void ExecuteTransactionListUserControlCommand()
        {
          //  CurrentViewModel = MainViewModel._EditTransactionModel;
        }



        public MainViewModel()
        {
            _selectAccount = new SelectAccount(this);
            SelectAccount = new RelayCommand<object>((parameter) => SwitchAccount(parameter));

            _save = new Save(this);
            Save = new RelayCommand<object>((parameter) => SaveAccounts(parameter));

            _visibilitySwitch = new VisibilitySwitch(this);
            VisibilitySwitch = new RelayCommand<object>((parameter) => SwitchVisibility(parameter));

            Transactions = new ObservableCollection<Transaction>();
            ChartValueList = new List<KeyValuePair<string, decimal>>();

          //  CurrentViewModel = MainViewModel._EditTransactionModel;
            EditTransactionUserControlerCommand = new RelayCommand(() => ExecuteEditTransactionUserControlerCommand());
            TransactionListUserControlCommand = new RelayCommand(() => ExecuteTransactionListUserControlCommand());

            var xml = ObjextXMLSerializer.GetInstance;
            Accounts = new ObservableCollection<Account>();
            try
            {
                Accounts = new ObservableCollection<Account>(xml.LoadAccounts());
            }
            catch (ArgumentException)
            {

            }

            Accounts = new ObservableCollection<Account>() {
                new Account() { Name = "Kristian",    Colour = new SolidColorBrush(Colors.CadetBlue)},
                new Account() { Name = "Darth Vader", Colour = new SolidColorBrush(Colors.Maroon)}
            };
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
            CurrentAccount = Accounts[1];
            xml.SaveAccounts(Accounts);

            samlingAfAccounts = CollectionViewSource.GetDefaultView(Accounts);

           foreach (var item in CurrentAccount.Transactions)
            {
                ChartValueList.Add(new KeyValuePair<string, decimal>(item.Description, item.Amount));
                
            }
    
            
    
        }

        void IDropTarget.DragOver(DropInfo dropInfo)
        {
            if (dropInfo.Data is Account && dropInfo.TargetItem is Account)
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