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
    public class Search : BaseCommand, ICommand
    {
        public Search(MainViewModel viewmodel)
        {
            this.Viewmodel = viewmodel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string searchword = parameter as string;
            IEnumerable<Transaction> searchResult = from item in Viewmodel.CurrentAccount.Transactions
                               where item.Description.ToLower().Contains(searchword.ToLower()) || item.Recipient.ToLower().Contains(searchword.ToLower())
                               select item;
            if (searchword.Equals(""))
                searchResult = Viewmodel.CurrentAccount.Transactions;

            Viewmodel.Transactions.Clear();
            foreach (var item in searchResult)
            {
                Viewmodel.Transactions.Add(item);
            }
        }
    }
}
