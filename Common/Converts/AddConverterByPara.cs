using Common.Log4;
using System;
using System.Windows.Data;

namespace Common.Converts
{
    [ValueConversion(typeof(double), typeof(double))]
    public class AddConverterByPara : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(double))
                throw new InvalidOperationException("The target must be a double");
            try
            {
                double para = double.Parse(parameter.ToString());
                return double.Parse(value.ToString()) + para;
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
