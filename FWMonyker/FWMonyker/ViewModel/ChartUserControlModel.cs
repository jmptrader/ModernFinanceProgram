/*
 ****************************************
 *                                      *
 *   Author: Michael Boe Larsen         *
 *           & Kristian Dam-Jensen      *
 *                                      *
 ****************************************
 */

using FWMonyker.Command;
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
                if (MainViewModel.CurrentAccount != null)
                {
                    foreach (var item in MainViewModel.CurrentAccount.Transactions)
                    {
                        list.Add(item.TimeStamp.ToShortDateString() +"\n"+ item.TimeStamp.ToLongTimeString(), item.BalanceAtTimeStamp);
                    }
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