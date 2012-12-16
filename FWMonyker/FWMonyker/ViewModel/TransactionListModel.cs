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
    public class TransactionListModel : ViewModelBase, INotifyPropertyChanged
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
        }

        public ICommand UndoCommand { get; private set; }

        public ICommand RedoCommand { get; private set; }

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
            var transaction = (parameter as Transaction) == null ? new Transaction() { Account = MainViewModel.CurrentAccount, Amount = 0, Description = "", Recipient = "", TimeStamp = DateTime.Now, BalanceAtTimeStamp = 0} : (parameter as Transaction).Clone();
            MainViewModel.CurrentViewModel = MainViewModel._EditTransactionModel;
            MainViewModel._EditTransactionModel.Transaction = transaction;
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