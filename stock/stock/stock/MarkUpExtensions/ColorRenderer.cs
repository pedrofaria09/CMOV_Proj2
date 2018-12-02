using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Diagnostics;
using System.Globalization;
using System.ComponentModel;
using System.Windows;

namespace stock.MarkUpExtensions
{
    public class ColorRenderer : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Color.FromRgba(255, 153, 102, 255);
            else
                return Color.FromRgba(255,225,255,0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "lol1";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {

            Debug.WriteLine("recebi update");
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler == null) return;
            handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
