/*
 ****************************************
 *                                      *
 *   Author: Kristian Dam-Jensen        *
 *                                      *
 ****************************************
 */

using FWMonyker.Model;
using FWMonyker.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace FWMonyker.Command
{
    public class Search : BaseCommand, ICommand
    {
        private TransactionListModel ViewModel;

        public Search(TransactionListModel viewmodel)
        {
            ViewModel = viewmodel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string searchword = parameter as string;

            ViewModel.MainViewModel.CurrentAccount.SetFilterKeyword(searchword);

        }
    }
}