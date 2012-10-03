using FWMonyker.Model;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace FWMonyker.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Account> Accounts { get; set; }
        public ObservableCollection<Transaction> Transactions { get; set; }

        public MainViewModel()
        {
            Accounts = new ObservableCollection<Account>() {
                new Account() { Name = "Kristian",    Colour = new SolidColorBrush(Colors.CadetBlue)},
                new Account() { Name = "Darth Vader", Colour = new SolidColorBrush(Colors.Maroon)}
            };

            Transactions = new ObservableCollection<Transaction>();

        }
    }
}