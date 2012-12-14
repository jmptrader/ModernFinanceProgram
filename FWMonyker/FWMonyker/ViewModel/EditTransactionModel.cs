using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using FWMonyker.Model;
using System.Diagnostics;
using System.Windows;
using System.ComponentModel;

namespace FWMonyker.ViewModel
{
    public class EditTransactionModel : ViewModelBase
    {
        private decimal _textAmount;

        public decimal textAmount
        {
            get
            {
                return _textAmount;
            }
            set
            {
                _textAmount = value;

                NotifyPropertyChanged("TextAmount");
            }
        }

        private string _textDescription;
        public string TextDescription
        {
            get
            {
                return _textDescription;
            }
            set
            {
                _textDescription = value;

                NotifyPropertyChanged("TextDescription");
            }
        }

        private string _textRecipient;

        public string textRecipient
        {
            get
            {
                return _textRecipient;
            }
            set
            {
                _textRecipient = value;

                NotifyPropertyChanged("TextDescription");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
