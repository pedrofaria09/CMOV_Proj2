using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using mystocks.Models;
using mystocks.Services;
using Xamarin.Forms;

namespace mystocks.ViewModels
{
    public class AddCityViewModel : BaseViewModel
    {
        public string SearchName { get; set; }
        public ObservableCollection<City> CitiesFound { get; set; }
        public City SelectedCity { get; set; }
        public Command SearchCitiesCommand { get; set; }

        public AddCityViewModel()
        {
            Title = "Add a new location";
            CitiesFound = new ObservableCollection<City>();
            SearchCitiesCommand = new Command(() => SearchCities());
        }

        private void SearchCities()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            CitiesFound.Clear();
            ApixAPI.SearchForCity(SearchName, SearchForCityHandler);
        }

        private void SearchForCityHandler(IAsyncResult asyncResult)
        {
            try
            {
                ApixAPI.CallHandler(asyncResult);
                var state = (State)asyncResult.AsyncState;
                if (state.Status == HttpStatusCode.OK)
                {
                    var cities = JsonConvert.DeserializeObject<List<City>>(state.Response);

                    foreach (var city in cities)
                    {
                        Device.BeginInvokeOnMainThread(() => { CitiesFound.Add(city); });
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
