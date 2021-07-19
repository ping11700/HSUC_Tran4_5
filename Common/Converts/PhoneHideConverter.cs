using System;
using System.Globalization;
using System.Windows.Data;

namespace Common.Converts
{
    [ValueConversion(typeof(string), typeof(string))]
    public class PhoneHideConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null && parameter == null)
                throw new ArgumentNullException("parameter can not be null");

            return ((string)value).Substring(0, 3) + "****" + ((string)value).Substring(7, 4);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
