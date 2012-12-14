﻿using System;
using System.Windows;
using System.Windows.Data;

namespace FWMonyker.Command.Converter
{
    public class VisibilityConverter : IValueConverter
    {
        #region Constructors

        /// <summary>
        /// The default constructor
        /// </summary>
        public VisibilityConverter()
        {
        }

        #endregion Constructors

        #region Properties

        public bool Collapse { get; set; }

        public bool Reverse { get; set; }

        #endregion Properties

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool bValue = (bool)value;

            if (bValue != Reverse)
            {
                return Visibility.Visible;
            }
            else
            {
                if (Collapse)
                    return Visibility.Collapsed;
                else
                    return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
                return !Reverse;
            else
                return Reverse;
        }

        #endregion IValueConverter Members
    }
}