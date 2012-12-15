using FWMonyker.Command;
using FWMonyker.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace FWMonyker.ViewModel
{
    public class TransactionListModel : ViewModelBase
    {
        public MainViewModel MainViewModel { get; private set; }

        private UndoRedoController undoRedoController = UndoRedoController.GetInstance();

        public TransactionListModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            _sort = new Sort(this);
            Sort = new RelayCommand<object>((parameter) => DoSort(parameter));

            _search = new Search(this);
            Search = new RelayCommand<object>((parameter) => DoSearch(parameter));

            EditTransactionUserControlerCommand = new RelayCommand<object>((parameter) => ExecuteEditTransactionUserControlerCommand(parameter));
            UndoCommand = new RelayCommand(undoRedoController.Undo, undoRedoController.CanUndo);
            RedoCommand = new RelayCommand(undoRedoController.Redo, undoRedoController.CanRedo);

            Transactions = new ObservableCollection<Transaction>();
        }

        public ICommand UndoCommand { get; private set; }

        public ICommand RedoCommand { get; private set; }

        public ObservableCollection<Transaction> Transactions { get; set; }

        private string _searchBox;

        public string SearchBox
        {
            get
            {
                return _searchBox;
            }
            set
            {
                _searchBox = value;
                NotifyPropertyChanged("SearchBox");
                Search.Execute(null);
            }
        }

        private ICommand _search;

        public ICommand Search { get; set; }

        public void DoSearch(object parameter)
        {
            _search.Execute(SearchBox);
        }

        public SortValues SortValue = SortValues.Ascending;
        private ICommand _sort;

        public ICommand Sort { get; set; }

        private void DoSort(object parameter)
        {
            _sort.Execute(SortValue);
        }

        public ICommand EditTransactionUserControlerCommand { get; private set; }

        private void ExecuteEditTransactionUserControlerCommand(object parameter)
        {
            var transaction = (parameter as Transaction) == null ? new Transaction() { Account = MainViewModel.CurrentAccount, Amount = 0, Description = "", Recipient = "", TimeStamp = DateTime.Now } : parameter as Transaction;
            MainViewModel.CurrentViewModel = MainViewModel._EditTransactionModel;
            if (parameter as Transaction == null)
            {
                MainViewModel._EditTransactionModel.Transaction = transaction;
            }
            else
            {
                var editableTransaction = new Transaction() { Account = transaction.Account, Amount = transaction.Amount, Description = transaction.Description, Recipient = transaction.Recipient, TimeStamp = transaction.TimeStamp };
                MainViewModel._EditTransactionModel.Transaction = editableTransaction;
            }
            MainViewModel._EditTransactionModel.initialStateTransaction = parameter as Transaction;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}