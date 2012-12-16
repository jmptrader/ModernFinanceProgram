using FWMonyker.Command;
using FWMonyker.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace FWMonyker.ViewModel
{
    public class EditAccountModel : ViewModelBase, IAccountChange, INotifyPropertyChanged
    {
        private MainViewModel MainViewModel;
        private UndoRedoController undoRedoController = UndoRedoController.GetInstance();

        public EditAccountModel(MainViewModel viewModel)
        {
            MainViewModel = viewModel;

            AddAccountCommand = new RelayCommand(AddAccount);
            DeleteAccountCommand = new RelayCommand(DeleteAccount);
        }

        public Account EndStateAccount;
        public Account InitialStateAccount;

        public string Name
        {
            get
            {
                return EndStateAccount.Name;
            }
            set
            {
                EndStateAccount.Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public decimal Balance
        {
            get
            {
                return EndStateAccount.Balance;
            }
            set
            {
                EndStateAccount.Balance = value;
                NotifyPropertyChanged("Balance");
            }
        }

        public Color Color
        {
            get
            {
                return EndStateAccount.Color;
            }
            set
            {
                EndStateAccount.Color = value;
                NotifyPropertyChanged("Color");
            }
        }

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

        public void NotifyAccountChange()
        {
            EndStateAccount = MainViewModel.CurrentAccount;
            InitialStateAccount = MainViewModel.CurrentAccount;

            NotifyPropertyChanged("Name");
            NotifyPropertyChanged("Color");
            NotifyPropertyChanged("Balance");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}