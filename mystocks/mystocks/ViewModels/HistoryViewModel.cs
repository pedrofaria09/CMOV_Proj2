using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using mystocks.Models;
using WeatherApp.Services;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        public Company company { get; set; }
        public ObservableCollection<StockDetails> stockDetails { get; set; }

        public HistoryViewModel(Company c = null)
        {
            Title = "Past Days";
            company = c;
            stockDetails = new ObservableCollection<StockDetails>();
        }

        /*DateTime selectedDate = DateTime.Today.AddDays(-1);
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                if (SetProperty(ref selectedDate, value))
                    LoadWeather();
            }
        }

        public DateTime MinimumDate
        {
            get { return DateTime.Today.AddDays(-30); }
        }

        public DateTime MaximumDate
        {
            get { return DateTime.Today.AddDays(-1); }
        }

        int minTemperature;
        public int MinTemperature
        {
            get { return minTemperature; }
            set { SetProperty(ref minTemperature, value); }
        }

        int maxTemperature;
        public int MaxTemperature
        {
            get { return maxTemperature; }
            set { SetProperty(ref maxTemperature, value); }
        }

        double maxWind;
        public double MaxWind
        {
            get { return maxWind; }
            set { SetProperty(ref maxWind, value); }
        }

        int avgHumidity;
        public int AvgHumidity
        {
            get { return avgHumidity; }
            set { SetProperty(ref avgHumidity, value); }
        }

        DateTime sunrise;
        public DateTime Sunrise
        {
            get { return sunrise; }
            set { SetProperty(ref sunrise, value); }
        }

        DateTime sunset;
        public DateTime Sunset
        {
            get { return sunset; }
            set { SetProperty(ref sunset, value); }
        }*/

        public void LoadWeather()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            API.getHistory(company.symbol, "20181101", LoadHistoryHandler);
        }

        private void LoadHistoryHandler(IAsyncResult asyncResult)
        {
            try
            {
                API.CallHandler(asyncResult);
                var state = (State)asyncResult.AsyncState;
                if (state.Status == HttpStatusCode.OK)
                {
                    Device.BeginInvokeOnMainThread(() => { stockDetails.Clear(); });

                    JArray response = JArray.Parse(state.Response);
                    foreach (JObject o in response["results"].Children<JObject>())
                    {
                        //DateTime date = o["tradingDay"];
                        float openValue = (float)o["open"];
                        float highValue = (float)o["high"];
                        float lowValue = (float)o["low"];
                        float closeValue = (float)o["close"];
                        int volume = (int)o["volume"];

                        StockDetails sd = new StockDetails(){ openValue = openValue, highValue = highValue, lowValue = lowValue, closeValue = closeValue, volume = volume };
                        stockDetails.Add(sd);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
