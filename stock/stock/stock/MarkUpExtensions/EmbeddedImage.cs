using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Diagnostics;
using System.Globalization;

namespace stock.MarkUpExtensions
{
    public class EmbeddedImage : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (String.IsNullOrWhiteSpace((String)value))
                return null;

            return ImageSource.FromResource("stock.Resources."+value+".png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "lol1";
        }

    }
}
