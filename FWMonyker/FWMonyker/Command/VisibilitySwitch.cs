using FWMonyker.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FWMonyker.Command
{
    public class VisibilitySwitch : BaseCommand, ICommand
    {
        public VisibilitySwitch(MainViewModel viewmodel)
        {
            this.Viewmodel = viewmodel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
        /// <summary>
        /// Blahaga
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            Viewmodel.UcVisibility = !Viewmodel.UcVisibility;
            Console.WriteLine("------ SWITCHED");
            //MessageBox.Show("Switching");
        }
    }
}
