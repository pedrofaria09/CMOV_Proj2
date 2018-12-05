using System;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace stock.Models
{
    public class CompanyStock : INotifyPropertyChanged
    {
        public string DisplayName { get; set; }

        private StockDetails _Details;
        public StockDetails Details
        {
            get
            {
                return _Details;
            }
            set
            {
                _Details = value;
                OnPropertyChanged(nameof(Details));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {

            Debug.WriteLine("recebi update de company stock");
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler == null) return;
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}