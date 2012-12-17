/*
 ****************************************
 *                                      *
 *   Author: Kristian Dam-Jensen        *
 *                                      *
 ****************************************
 */

using FWMonyker.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace FWMonyker.Command
{
    public class Save : BaseCommand, ICommand
    {
        private MainViewModel ViewModel;
        private BackgroundWorker worker;

        public Save(MainViewModel viewmodel)
        {
            ViewModel = viewmodel;
        }

        public bool CanExecute(object parameter)
        {
            return !worker.IsBusy;
        }

        public void Execute(object parameter)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_SaveToXML);
            worker.RunWorkerAsync();
        }

        public void worker_SaveToXML(object sender, DoWorkEventArgs e)
        {
            XML.ObjextXMLSerializer.GetInstance.SaveAccounts(ViewModel.Accounts);
        }
    }
}