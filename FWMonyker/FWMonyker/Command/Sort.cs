﻿using FWMonyker.Model;
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
            var list = ViewModel.Transactions;
            var result = list.ToList();
            switch (ViewModel.SortValue)
            {
                case SortValues.Ascending:
                    result = (from item in list
                              orderby item.TimeStamp.Ticks ascending
                              select item).ToList();
                    ViewModel.SortValue = SortValues.Descending;
                    break;
                case SortValues.Descending:
                    result = (from item in list
                           orderby item.TimeStamp.Ticks descending
                           select item).ToList();
                    ViewModel.SortValue = SortValues.Ascending;
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
    public enum SortValues
    {
        Ascending,
        Descending
    }
}
