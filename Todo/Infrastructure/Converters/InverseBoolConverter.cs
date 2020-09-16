using System;
using Xamarin.Forms;
using System.Globalization;

namespace Todo.Infrastructure.Converters
{
    public class InverseBoolConverter : IValueConverter
    {
        #region IValueConverter Implementation

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        #endregion
    }
}
