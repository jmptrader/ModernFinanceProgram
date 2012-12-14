using FWMonyker.Model;
using FWMonyker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FWMonyker.Command
{
    class SaveTransaction : BaseCommand, ICommand
    {
        public SaveTransaction(EditTransactionModel viewmodel)
        {
            //Viewmodel = viewmodel;
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            //var item = new Transaction() {
            //    Account = Viewmodel.CurrentAccount,
            //    Description = Viewmodel.AddTransactionDescription,
            //    Amount = Viewmodel.AddTransactionAmount,
            //    Recipient = Viewmodel.AddTransactionAmount,
            //    TimeStamp = DateTime.Now};
            //}
        }
    }
}
