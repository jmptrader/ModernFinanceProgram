using FWMonyker.Command;
using FWMonyker.Model;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;

namespace FWMonyker.ViewModel
{
    public class ChartUserControlModel : ViewModelBase, INotifyPropertyChanged, IAccountChange
    {
        public MainViewModel MainViewModel { get; private set; }

        public ChartUserControlModel(MainViewModel viewModel)
        {
            MainViewModel = viewModel;
        }
        public Dictionary<string, decimal> ChartValueList
        {
            get
            {
                Dictionary<string, decimal> list = new Dictionary<string, decimal>();
                foreach (var item in MainViewModel.CurrentAccount.Transactions)
                {
                    list.Add(item.Description, item.Amount);
                }
                return list;
            }
        }

        public void NotifyAccountChange()
        {
            NotifyPropertyChanged("ChartValueList");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}