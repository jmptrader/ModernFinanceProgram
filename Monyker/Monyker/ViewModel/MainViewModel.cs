using Monyker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Monyker.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<Account> Accounts { get; set; }
        public ObservableCollection<Transaction> Transactions { get; set; }

        public MainViewModel()
        {
            Accounts = new ObservableCollection<Account>() {
                new Account() { Name = "Kristian", Colour = new SolidColorBrush(Colors.LimeGreen) },
                new Account() { Name = "Darth Vader", Colour = new SolidColorBrush(Colors.CadetBlue) }
            };

            Transactions = new ObservableCollection<Transaction>();

        }
    }
}
