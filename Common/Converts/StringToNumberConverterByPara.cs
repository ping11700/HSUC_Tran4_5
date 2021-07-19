using Common.Log4;
using System;
using System.Windows.Data;

namespace Common.Converts
{
    [ValueConversion(typeof(string), typeof(double))]
    public class StringToNumberConverterByPara : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new InvalidOperationException("The target must be a string");
            try
            {
                double para = double.Parse(parameter.ToString());
                return double.Parse(value.ToString()) * para;
            }
            catch (Exception ex)
            {
                LogService.ILogger.Warn($"IValueConverter:{ex.Message}");
                throw;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
