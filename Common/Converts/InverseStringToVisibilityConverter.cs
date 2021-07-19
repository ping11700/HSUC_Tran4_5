using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Common.Converts
{
    [ValueConversion(typeof(string), typeof(Visibility))]
    public class InverseStringToVisibilityConverter : IValueConverter
    {
        //parameter ==0 hidden
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a visibility");

            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
