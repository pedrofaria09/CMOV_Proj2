using System;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace stock.Models
{
    public class Company : INotifyPropertyChanged
    {
        public string Symbol { get; set; }
        public string DisplayName { get; set; }
        //public bool IsSelected { get; set; }

        private bool _IsSelected;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {

            Debug.WriteLine("recebi update");
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler == null) return;
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}