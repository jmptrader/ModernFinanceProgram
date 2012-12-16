/*
 ****************************************
 *                                      *
 *   Author: Kristian Dam-Jensen        *
 *                                      *
 ****************************************
 */

using System.ComponentModel;

namespace FWMonyker.Model
{
    public abstract class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}