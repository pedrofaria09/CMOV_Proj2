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

        public HistoryViewModel(List<Company> companies = null)
        {
            Title = "Past Days";
            //company = c;
            CompaniesSelected = companies;
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
                API.getHistory(CompaniesSelected[i].Symbol, "20181101", LoadHistoryHandler);
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
                        DateTime date = DateTime.ParseExact((String)o["tradingDay"], "yyyy-MM-dd",null);
                        float openValue = (float)o["open"];
                        float highValue = (float)o["high"];
                        float lowValue = (float)o["low"];
                        float closeValue = (float)o["close"];
                        int volume = (int)o["volume"];
                        symbol = (string)o["symbol"];

                        Debug.WriteLine("recebi " + openValue + " " + highValue + " " + lowValue + " " + closeValue + " " + volume);

                        StockDetails sd = new StockDetails() { openValue = openValue, highValue = highValue, lowValue = lowValue, closeValue = closeValue, volume = volume };
                        details.Add(sd);
                        
                    }
                    for(int i=0;i< CompaniesSelected.Count; i++)
                    {
                        if (CompaniesSelected[i].Symbol == symbol)
                        {
                            CompanyStock Cs = new CompanyStock() { DisplayName = CompaniesSelected[i].DisplayName, Details = details[0] };
                            CompaniesStock.Add(Cs);
                        }
                    }
                    
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Debug.WriteLine("vou autorizar " + details.Count);
                        CanDraw = true;
                        stockDetails.Add(details);
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
