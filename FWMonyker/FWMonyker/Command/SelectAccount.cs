using FWMonyker.Model;
using FWMonyker.ViewModel;
using System.Linq;
using System.Windows.Input;

namespace FWMonyker.Command
{
    public class SelectAccount : BaseCommand, ICommand
    {
        private MainViewModel ViewModel;

        public SelectAccount(MainViewModel viewmodel)
        {
            ViewModel = viewmodel;
        }

        public bool CanExecute(object parameter)
        {
            return true;

            //Add conditions here in cases where it should not be allowed to switch account
        }

        /// <summary>
        /// Sets the current account in the viewmodel
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (parameter is Account)
            {
                int nr = (parameter as Account).Transactions != null ? (parameter as Account).Transactions.ToList().Count : 0;
                ViewModel.CurrentAccount = (parameter as Account);
            }
        }
    }
}