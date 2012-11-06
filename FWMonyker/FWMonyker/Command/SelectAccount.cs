using FWMonyker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FWMonyker.Command
{
    public class SelectAccount : ICommand
    {
        private Account CurrentAccount;
        private bool _canExecute;

        public SelectAccount(Account currentAccount)
        {
            CurrentAccount = currentAccount;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
            //Add conditions here in cases where it should not be allowed to switch account
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _canExecute = false;
            CurrentAccount = (parameter as Account);
            _canExecute = true;
        }
    }
}
