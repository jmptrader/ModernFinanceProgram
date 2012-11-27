using FWMonyker.Model;
using FWMonyker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FWMonyker.Command
{
    public class SelectAccount : ICommand
    {
        private MainViewModel Viewmodel;

        public SelectAccount(MainViewModel viewmodel)
        {
            Viewmodel = viewmodel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
            //Add conditions here in cases where it should not be allowed to switch account
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (parameter is Account)
            {
                int nr = (parameter as Account).Transactions != null ? (parameter as Account).Transactions.ToList().Count : 0;                
                Viewmodel.CurrentAccount = (parameter as Account);
                
            }
        }
    }
}
