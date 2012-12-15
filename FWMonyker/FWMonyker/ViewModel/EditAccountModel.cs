using FWMonyker.Command;
using FWMonyker.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FWMonyker.ViewModel
{
    public class EditAccountModel : ViewModelBase
    {
        private MainViewModel MainViewModel;
        private UndoRedoController undoRedoController = UndoRedoController.GetInstance();

        public EditAccountModel(MainViewModel viewModel)
        {
            MainViewModel = viewModel;

            AddAccountCommand = new RelayCommand(AddAccount);
            DeleteAccountCommand = new RelayCommand(DeleteAccount);
        }

        public Account EndStateAccount { get; set; }
        public Account InitialStateAccount;

        public ICommand AddAccountCommand { get; private set; }
        public void AddAccount()
        {
            undoRedoController.AddAndExecute(new AddAccountCommand(MainViewModel.Accounts, InitialStateAccount, EndStateAccount));
            MainViewModel.CurrentViewModel = MainViewModel._TransactionListModel;
            MainViewModel.Save.Execute(null);
        }
        public ICommand DeleteAccountCommand { get; private set; }
        public void DeleteAccount()
        {
            undoRedoController.AddAndExecute(new DeleteAccountCommand(MainViewModel.Accounts, InitialStateAccount));
            MainViewModel.CurrentViewModel = MainViewModel._TransactionListModel;
            MainViewModel.Save.Execute(null);
        }
    }
}
