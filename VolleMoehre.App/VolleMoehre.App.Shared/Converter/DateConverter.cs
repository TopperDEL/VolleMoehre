using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Windows.UI.Xaml.Data;

namespace VolleMoehre.App.Shared.Converter
{
    public class DateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            DateTime dt = DateTime.Parse(value.ToString());
            return dt.ToString("f") + " Uhr";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
