using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ConvertersDemo.Converters
{
    class IntToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                switch (value)
                {
                    case 0:
                        return "newyork.jpg";
                    case 1:
                        return "tokyo.jpg";
                    case 2:
                        return "prague.jpg";
                    case 3:
                        return "paris.jpg";
                    case 4:
                        return "london.jpg";
                    case 5:
                        return "berlin.jpg";
                    default:
                        break;
                }
            }
            return default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
