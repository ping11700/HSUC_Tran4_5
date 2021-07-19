using System;
using System.Windows;
using System.Windows.Data;

namespace Common.Converts
{
    [ValueConversion(typeof(object), typeof(Thickness))]
    public class MarginConverterByPara : IValueConverter
    {

        /// <summary>
        /// parameter =0 margin  margin=0,0,0,0
        /// parameter =1 margin  单边
        /// parameter =2 margin  左右双侧
        /// parameter =3 margin  上下双侧
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Thickness))
                throw new InvalidOperationException("The target must be a Thickness");
            int marginType;
            double number = (double)value;

            if (int.TryParse(parameter.ToString(), out marginType))
            {
                switch (marginType)
                {
                    case 0:
                        return new System.Windows.Thickness(0, 0, 0, 0);
                    case 1:
                        return new System.Windows.Thickness(number, 0, 0, 0);
                    case 2:
                        return new System.Windows.Thickness(number, 0, number, 0);
                    case 3:
                        return new System.Windows.Thickness(0, number, 0, number);
                    default:
                        return new System.Windows.Thickness(0, 0, 0, 0);
                }
            }
            else
            {
                return new System.Windows.Thickness(0, 0, 0, 0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
