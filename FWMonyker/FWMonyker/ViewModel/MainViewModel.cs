using FWMonyker.Model;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace FWMonyker.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Account> Accounts { get; set; }
        public Account CurrentAccount { get; set; }

        public ICommand SelectAccount { get; set; }

        public void SwitchAccount(object parameter)
        {
            //TODO: Parameter needs to be the account, its currently the button.
            Console.WriteLine(parameter.GetType().ToString());
            //if (SelectAccount.CanExecute(null))
            //    SelectAccount.Execute(parameter);
        }
        public MainViewModel()
        {
            //Starts debugging console
            //ConsoleManager.Show();

            SelectAccount = new RelayCommand<object>((parameter) => SwitchAccount(parameter));

            Accounts = new ObservableCollection<Account>() {
                new Account() { Name = "Kristian",    Colour = new SolidColorBrush(Colors.CadetBlue)},
                new Account() { Name = "Darth Vader", Colour = new SolidColorBrush(Colors.Maroon)}
            };
            Console.WriteLine(Accounts.ToString());
            CurrentAccount = Accounts[1];
            CurrentAccount.Transactions = new ObservableCollection<Transaction>() {
                new Transaction() { Account = Accounts[1] , Description = "noget", Amount = 1000, 
                    Recipient = "nogle", TimeStamp = DateTime.Now},
                new Transaction() { Account = Accounts[0] , Description = "noget1", Amount = 1001, 
                    Recipient = "nogle1", TimeStamp = DateTime.Now},
            };

        }
    }
}