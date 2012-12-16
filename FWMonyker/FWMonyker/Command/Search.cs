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
            IEnumerable<Transaction> searchResult = from item in ViewModel.MainViewModel.CurrentAccount.Transactions
                                                    where item.Description.ToLower().Contains(searchword.ToLower()) || item.Recipient.ToLower().Contains(searchword.ToLower())
                                                    select item;
            if (string.IsNullOrWhiteSpace(searchword))
                searchResult = ViewModel.MainViewModel.CurrentAccount.Transactions;

            ViewModel.MainViewModel.CurrentAccount.Transactions.Clear();
            foreach (var item in searchResult)
            {
                ViewModel.MainViewModel.CurrentAccount.Transactions.Add(item);
            }
        }
    }
}