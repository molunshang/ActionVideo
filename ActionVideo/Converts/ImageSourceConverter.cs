using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ActionVideo.Converts
{
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Uri.TryCreate((string)value, UriKind.Absolute, out var uri) ? new UriImageSource() { Uri = uri, CachingEnabled = true, CacheValidity = TimeSpan.FromDays(30) } : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
