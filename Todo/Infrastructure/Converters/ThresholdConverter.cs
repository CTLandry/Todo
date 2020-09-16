using System;
using System.Globalization;
using Xamarin.Forms;

namespace Todo.Infrastructure.Converters
{
    public class ThresholdConverter : IValueConverter
    {
        public int Threshold { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        { 
            return ((int)value) >= Threshold;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
}
