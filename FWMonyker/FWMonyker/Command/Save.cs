using FWMonyker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FWMonyker.Command
{
    public class Save : BaseCommand, ICommand
    {
        public Save(MainViewModel viewmodel)
        {
            this.Viewmodel = viewmodel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
            //Add conditions here in cases where it should not be allowed to switch account
        }

        public override void Execute(object parameter)
        {
            XML.ObjextXMLSerializer.GetInstance.SaveAccounts(Viewmodel.Accounts);
        }
    }
}
