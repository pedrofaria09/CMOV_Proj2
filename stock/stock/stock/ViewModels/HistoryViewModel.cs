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
        public ObservableCollection<List<StockDetails>> stockDetails { get; set; }
        public bool CanDraw { get; set; }
        List<Company> CompaniesSelected;
        public ObservableCollection<CompanyStock> CompaniesStock { get; set; }
        String date;

        string  SliderDate_;
        public string SliderDate
        {
            get { return SliderDate_; }
            set
            {
                SetProperty(ref SliderDate_, value);
            }
        }

        double Minimum_ = 0;
        public double Minimum_Slider
        {
            get { return Minimum_; }
            set { SetProperty(ref Minimum_, value); }
        }

        double Maximum_ = 100;
        public double Maximum_Slider
        {
            get { return Maximum_; }
            set { SetProperty(ref Maximum_, value); }
        }

        double SliderValue_ = 50;
        public double SliderValue
        {
            get { return SliderValue_; }
            set {
                SetProperty(ref SliderValue_, value);
                Slider_ValueChanged(); 
            }
        }


        public HistoryViewModel(List<Company> companies = null, String date = null)

        {
            Title = "Past Days";
            //company = c;
            CompaniesSelected = companies;
            this.date = date;
            stockDetails = new ObservableCollection<List<StockDetails>>();
            CompaniesStock = new ObservableCollection<CompanyStock>();
        }

        public void LoadHistory()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            for(int i=0;i< CompaniesSelected.Count; i++)
            {
                API.getHistory(CompaniesSelected[i].Symbol, date, LoadHistoryHandler);
            }
            
        }

        private void LoadHistoryHandler(IAsyncResult asyncResult)
        {
            try
            {
                CanDraw = false;
                API.CallHandler(asyncResult);
                var state = (State)asyncResult.AsyncState;
                string symbol = "";
                if (state.Status == HttpStatusCode.OK)
                {
                    Device.BeginInvokeOnMainThread(() => { stockDetails.Clear(); });
                    Debug.WriteLine("rsponse " + state.Response);
                    JObject response = JObject.Parse(state.Response);
                    List<StockDetails> details = new List<StockDetails>();
                    foreach (JObject o in response["results"].Children<JObject>())
                    {
                        DateTime date = DateTime.ParseExact((string)o["tradingDay"], "yyyy-MM-dd",null);
                        Debug.WriteLine("data " + date + " " + o["tradingDay"]);
                        float openValue = (float)o["open"];
                        float highValue = (float)o["high"];
                        float lowValue = (float)o["low"];
                        float closeValue = (float)o["close"];
                        int volume = (int)o["volume"];
                        symbol = (string)o["symbol"];

                        Debug.WriteLine("recebi " + openValue + " " + highValue + " " + lowValue + " " + closeValue + " " + volume);

                        StockDetails sd = new StockDetails() { date = date, openValue = openValue, highValue = highValue, lowValue = lowValue, closeValue = closeValue, volume = volume };
                        details.Add(sd);
                        
                    }

                    CompanyStock Cs = null;
                    for (int i=0;i< CompaniesSelected.Count; i++)
                    {
                        if (CompaniesSelected[i].Symbol == symbol)
                        {
                            Cs = new CompanyStock() { DisplayName = CompaniesSelected[i].DisplayName, Details = details[0] };
                            
                        }
                    }
                    SliderDate = details[0].date.ToString("dd/MM/yyyy");
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        CompaniesStock.Add(Cs);
                        Debug.WriteLine("vou autorizar " + details.Count);
                        CanDraw = true;
                        stockDetails.Add(details);
                        SliderDate = stockDetails[0][0].date.ToString("dd/MM/yyyy");
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

        public void Slider_ValueChanged()
        {
            if(stockDetails!=null && stockDetails.Count > 0 && CompaniesStock!=null && CompaniesStock.Count>0 && CompaniesStock.Count == stockDetails.Count)
            {
                int maxValue = stockDetails[0].Count;
                int position = (int)Math.Floor(SliderValue_ / (100 / (double)maxValue));
                
                if (position >= maxValue)
                    position = maxValue - 1;
                SliderDate = stockDetails[0][position].date.ToString("dd/MM/yyyy");
                Debug.WriteLine("pqp pra esta merda " + CompaniesStock.Count + " " + stockDetails.Count);
                for (int i=0;i< CompaniesStock.Count; i++)
                {
                    if(i< stockDetails.Count)
                        CompaniesStock[i].Details = stockDetails[i][position];
                }

            }
            
        }
    }
}
