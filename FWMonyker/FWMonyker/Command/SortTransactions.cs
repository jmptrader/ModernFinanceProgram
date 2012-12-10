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
    public class SortTransactions : BaseCommand, ICommand
    {
        public SortTransactions(MainViewModel viewmodel)
        {
            Viewmodel = viewmodel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            IEnumerable<Transaction> list = Viewmodel.Transactions;
            IEnumerable<Transaction> result = list;
            switch (parameter as string)
            {
                case "ascending":
                    result = from item in list
                           orderby item.TimeStamp.Ticks ascending
                           select item;
                    Viewmodel.SortValue = "descending";
                    break;
                case "descending":
                    result = from item in list
                           orderby item.TimeStamp.Ticks descending
                           select item;
                    Viewmodel.SortValue = "ascending";
                    break;
            }
            result = result.ToList();
            Viewmodel.Transactions.Clear();
            foreach (var item in result)
            {
                Viewmodel.Transactions.Add(item);
            }
            
        }
    }
}
