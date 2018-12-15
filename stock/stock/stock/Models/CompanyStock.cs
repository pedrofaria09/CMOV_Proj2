using System;
using System.ComponentModel;
using System.Diagnostics;

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

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            try
            {
                Debug.WriteLine("recebi update de company stock");
                PropertyChangedEventHandler handler = PropertyChanged;
                Debug.WriteLine("passei " + PropertyChanged);

                if (handler == null) return;
                Debug.WriteLine("nao é nulo");
                handler(this, new PropertyChangedEventArgs(propertyName));
                Debug.WriteLine("passei 1");
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        #endregion
    }
}