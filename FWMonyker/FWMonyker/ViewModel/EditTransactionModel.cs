/*
 ****************************************
 *                                      *
 *   Author: Kristian Dam-Jensen        *
 *                                      *
 ****************************************
 */

using FWMonyker.Command;
using FWMonyker.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
                Transaction.BalanceAtTimeStamp = Transaction.Account.Balance + Transaction.Amount;
                undoRedoController.AddAndExecute(new AddTransaction(MainViewModel.CurrentAccount.Transactions, initialStateTransaction, Transaction));
                MainViewModel.CurrentViewModel = MainViewModel._TransactionListModel;
                Clear();
                MainViewModel.Save.Execute(null);
        }

        public ICommand DeleteTransactionCommand { get; private set; }

        public void DeleteTransaction()
        {
            undoRedoController.AddAndExecute(new DeleteTransaction(MainViewModel.CurrentAccount.Transactions, initialStateTransaction));
            MainViewModel.CurrentViewModel = MainViewModel._TransactionListModel;
            Clear();
            MainViewModel.Save.Execute(null);
        }

        public void Clear()
        {
            Transaction = null;
            initialStateTransaction = null;
        }
    }
}