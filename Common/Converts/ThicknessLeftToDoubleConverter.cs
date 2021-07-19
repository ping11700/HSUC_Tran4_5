using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Common.Converts
{
    /// <summary>
    /// 获取Thickness固定值double
    /// </summary>
    [ValueConversion(typeof(Thickness), typeof(double))]
    public class ThicknessLeftToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var thickness = (Thickness)value;
                return thickness.Left;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
