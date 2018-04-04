using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace CruiseBookingApp.Converters
{
    public class ValidatableObjectErrorMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var errors = (List<string>)value;

            return errors?.FirstOrDefault();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
