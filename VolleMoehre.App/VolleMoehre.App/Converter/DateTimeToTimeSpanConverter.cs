using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.UI.Xaml.Data;

namespace VolleMoehre.App.Shared.Converter
{
    public class DateTimeToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                DateTime dt = (DateTime)value;
                TimeSpan? ts = DateTimeConverter.DateTimeToTimeSpan(dt);
                return ts.GetValueOrDefault(TimeSpan.MinValue);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return TimeSpan.MinValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                TimeSpan ts = (TimeSpan)value;
                DateTime? dt = DateTimeConverter.TimeSpanToDateTime(ts);
                return dt.GetValueOrDefault(DateTime.MinValue);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return DateTime.MinValue;
            }
        }
    }
}
