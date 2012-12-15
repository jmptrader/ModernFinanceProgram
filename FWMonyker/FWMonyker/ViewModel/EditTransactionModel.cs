using FWMonyker.Command;
using FWMonyker.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Windows.Input;

namespace FWMonyker.ViewModel
{
    public class EditTransactionModel : ViewModelBase
    {
        private MainViewModel MainViewModel;
        private UndoRedoController undoRedoController = UndoRedoController.GetInstance();

        public EditTransactionModel(MainViewModel viewModel)
        {
            MainViewModel = viewModel;
            AddTransactionCommand = new RelayCommand(AddTransaction);
            DeleteTransactionCommand = new RelayCommand(DeleteTransaction);
        }

        public Transaction Transaction { get; set; }

        public Transaction initialStateTransaction;

        public ICommand AddTransactionCommand { get; private set; }

        public void AddTransaction()
        {
            undoRedoController.AddAndExecute(new AddTransaction(MainViewModel.CurrentAccount.Transactions, MainViewModel._TransactionListModel.Transactions, initialStateTransaction, Transaction));
            MainViewModel.CurrentViewModel = MainViewModel._TransactionListModel;
            MainViewModel.Save.Execute(null);
        }

        public ICommand DeleteTransactionCommand { get; private set; }

        public void DeleteTransaction()
        {
            undoRedoController.AddAndExecute(new DeleteTransaction(MainViewModel.CurrentAccount.Transactions as IList<Transaction>, MainViewModel._TransactionListModel.Transactions, initialStateTransaction));
            MainViewModel.CurrentViewModel = MainViewModel._TransactionListModel;
            MainViewModel.Save.Execute(null);
        }
    }
}