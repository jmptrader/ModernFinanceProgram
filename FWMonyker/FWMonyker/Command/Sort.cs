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
    public class Sort : BaseCommand, ICommand
    {
        private TransactionListModel ViewModel;

        public Sort(TransactionListModel viewmodel)
        {
            ViewModel = viewmodel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            IEnumerable<Transaction> list = ViewModel.Transactions;
            IEnumerable<Transaction> result = list;
            switch (parameter as string)
            {
                case "ascending":
                    result = from item in list
                           orderby item.TimeStamp.Ticks ascending
                           select item;
                    ViewModel.SortValue = "descending";
                    break;
                case "descending":
                    result = from item in list
                           orderby item.TimeStamp.Ticks descending
                           select item;
                    ViewModel.SortValue = "ascending";
                    break;
            }
            result = result.ToList();
            ViewModel.Transactions.Clear();
            foreach (var item in result)
            {
                ViewModel.Transactions.Add(item);
            }
            
        }
    }
}
