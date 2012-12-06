using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FWMonyker.Command
{
    public class BoolVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Console.WriteLine("------ CONVERTER");
            bool isVisible = (bool)value;
            return isVisible ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Windows.Visibility currVisibility = (System.Windows.Visibility)value;
            return (currVisibility == System.Windows.Visibility.Visible);
        }
    }
}
