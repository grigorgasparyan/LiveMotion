using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LiveMotion.WPFCliet.Converters
{
    public class PlayPauseImgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = value is bool && (bool)value;
            if(result)
            {
                return @"..\..\Images\pause.png";
            }
            else
            {
                return @"..\..\Images\play-next-button.png";               
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
