using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using FWMonyker.Model;
using System.Diagnostics;
using System.Windows;
using System.ComponentModel;
using FWMonyker.Command;

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
            UndoCommand = new RelayCommand(undoRedoController.Undo, undoRedoController.CanUndo);
            RedoCommand = new RelayCommand(undoRedoController.Redo, undoRedoController.CanRedo);
        }

        public Transaction Transaction { get; set; }

        public ICommand AddTransactionCommand { get; private set; }
        public void AddTransaction()
        {
            if (Transaction != null)
                undoRedoController.AddAndExecute(new AddTransaction(MainViewModel.CurrentAccount.Transactions, MainViewModel._TransactionListModel.Transactions, Transaction));
            MainViewModel.CurrentViewModel = MainViewModel._TransactionListModel;
        }
        public ICommand DeleteTransactionCommand { get; private set; }
        public void DeleteTransaction()
        {
            undoRedoController.AddAndExecute(new DeleteTransaction(MainViewModel.CurrentAccount.Transactions as IList<Transaction>, MainViewModel._TransactionListModel.Transactions, Transaction));
        }
        public ICommand UndoCommand { get; private set; }
        public ICommand RedoCommand { get; private set; }
    }
}
