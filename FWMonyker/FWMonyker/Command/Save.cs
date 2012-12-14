using FWMonyker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FWMonyker.Command
{
    public class Save : BaseCommand, ICommand
    {
        private MainViewModel ViewModel;

        public Save(MainViewModel viewmodel)
        {
            ViewModel = viewmodel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            XML.ObjextXMLSerializer.GetInstance.SaveAccounts(ViewModel.Accounts);
        }
    }
}
