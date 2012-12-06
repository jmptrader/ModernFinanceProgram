using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace FWMonyker.ViewModel
{
     //<summary>
     //This class contains static references to all the view models in the
     //application and provides an entry point for the bindings.
     //<para>
     //See http://www.galasoft.ch/mvvm
     //</para>
     //</summary>
    public class ViewModelLocator
    {
        private static MainViewModel _main;
        public ViewModelLocator()
        {
            //// Denne ServiceLocator holder styr på vores ViewModels.
            //ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //// Tilføj en linje her for hver ViewModel.
            //SimpleIoc.Default.Register<MainViewModel>();
            _main = new MainViewModel();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return _main;
                //return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }

}