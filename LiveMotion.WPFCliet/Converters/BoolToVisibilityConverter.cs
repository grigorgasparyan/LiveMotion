using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LiveMotion.WPFCliet.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = result = value is bool && (bool)value;
            if((string)parameter == "!")
            {
                result = !result;
            }
            if (result)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
