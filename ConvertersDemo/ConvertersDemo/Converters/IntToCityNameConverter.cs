using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ConvertersDemo.Converters
{
    class IntToCityNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                switch (value)
                {
                    case 0:
                        return "New York";
                    case 1:
                        return "Tokyo";
                    case 2:
                        return "Prague";
                    case 3:
                        return "Paris";
                    case 4:
                        return "London";
                    case 5:
                        return "Berlin";
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