using FWMonyker.Command;
using FWMonyker.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FWMonyker.ViewModel
{
    public class TransactionListModel : ViewModelBase
    {

        public MainViewModel MainViewModel { get; private set; }

        public TransactionListModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            _sort = new Sort(this);
            Sort = new RelayCommand<object>((parameter) => DoSort(parameter));

            _search = new Search(this);
            Search = new RelayCommand<object>((parameter) => DoSearch(parameter));

            Transactions = new ObservableCollection<Transaction>();
        }

        public ObservableCollection<Transaction> Transactions { get; set; }

        private string _searchBox;
        public string SearchBox
        {
            get
            {
                return _searchBox;
            }
            set
            {
                _searchBox = value;
                NotifyPropertyChanged("SearchBox");
                Search.Execute(null);
            }
        }

        ICommand _search;
        public ICommand Search { get; set; }

        public void DoSearch(object parameter)
        {
            _search.Execute(SearchBox);
        }

        public string SortValue = "ascending";
        ICommand _sort;
        public ICommand Sort { get; set; }

        private void DoSort(object parameter)
        {
            _sort.Execute(SortValue);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
