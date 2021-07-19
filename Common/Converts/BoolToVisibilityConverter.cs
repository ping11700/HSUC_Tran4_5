using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Common.Converts
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : IValueConverter
    {
        //parameter ==0 hidden
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a visibility");

            if (parameter != null)
            {
                if (parameter.ToString() == "0")
                {
                    return value is bool && (bool)value ? Visibility.Visible : Visibility.Hidden;
                }
                else
                {
                    return value is bool && (bool)value ? Visibility.Visible : Visibility.Collapsed;
                }
            }
            else
            {
                return value is bool && (bool)value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
