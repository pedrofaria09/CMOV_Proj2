using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using stock.Models;
using stock.Services;
using Xamarin.Forms;



namespace stock.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        public Company company { get; set; }
        public ObservableCollection<StockDetails> stockDetails { get; set; }
        public bool CanDraw { get; set; }

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

        public void LoadHistory()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            API.getHistory(company.Symbol, "20181101", LoadHistoryHandler);
        }

        private void LoadHistoryHandler(IAsyncResult asyncResult)
        {
            try
            {
                CanDraw = false;
                API.CallHandler(asyncResult);
                var state = (State)asyncResult.AsyncState;
                if (state.Status == HttpStatusCode.OK)
                {
                    Device.BeginInvokeOnMainThread(() => { stockDetails.Clear(); });
                    Debug.WriteLine("rsponse " + state.Response);
                    JObject response = JObject.Parse(state.Response);
                    List<StockDetails> details = new List<StockDetails>();
                    foreach (JObject o in response["results"].Children<JObject>())
                    {
                        DateTime date = DateTime.ParseExact((String)o["tradingDay"], "yyyy-MM-dd",null);
                        float openValue = (float)o["open"];
                        float highValue = (float)o["high"];
                        float lowValue = (float)o["low"];
                        float closeValue = (float)o["close"];
                        int volume = (int)o["volume"];

                        Debug.WriteLine("recebi " + openValue + " " + highValue + " " + lowValue + " " + closeValue + " " + volume);

                        StockDetails sd = new StockDetails() { openValue = openValue, highValue = highValue, lowValue = lowValue, closeValue = closeValue, volume = volume };
                        details.Add(sd);
                        
                    }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        for (int i = 0; i < details.Count; i++)
                        {
                            if (i == details.Count - 1)
                                CanDraw = true;
                            stockDetails.Add(details[i]);
                        }
                    });
                    
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
