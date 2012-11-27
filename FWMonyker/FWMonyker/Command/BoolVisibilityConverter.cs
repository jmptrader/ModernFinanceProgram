using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FWMonyker.Command
{
    public class BoolVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            try
            {
                var isVisible = System.Convert.ToBoolean(value);
                return isVisible ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            } catch(Exception) {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                System.Windows.Visibility currVisibility = (System.Windows.Visibility)value;
                return (currVisibility == System.Windows.Visibility.Visible);
            }
            catch (Exception) {
                return System.Windows.Visibility.Visible;
            }
        }
    }
}
