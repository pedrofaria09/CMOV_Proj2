using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using stock.Models;
using stock.Views;
using System.Net;
using System.IO;
using System.Linq;
//using stock.Services;

using System.Reflection;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace stock.ViewModels
{
    public class CompaniesListViewModel : BaseViewModel
    {
        public ObservableCollection<Company> Companies { get; set; }

        public CompaniesListViewModel()
        {
            Title = "Companies";

            //Minimum_Slider = 10;
            //Maximum_Slider = 50;
            SliderValue = 20;

            Companies = new ObservableCollection<Company>();

            loadCompanies();

            
            // ...
            // NOTE: use for debugging, not in released app code!
            var assembly = typeof(CompaniesListPage).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
            {
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            }

            /*
            MessagingCenter.Subscribe<AddCityPage, City>(this, "AddCity", async (obj, item) =>
            {
                var _item = item as City;
                _item.DisplayName = _item.Name.Split(',').FirstOrDefault();
                await DataStore.AddCityAsync(_item);
            });*/
        }

        private void loadCompanies()
        {
            Company c = new Company() { DisplayName = "Apple", Symbol = "AAPL", IsSelected = false };
            Companies.Add(c);
            c = new Company() { DisplayName = "IBM", Symbol = "IBM", IsSelected = false };
            Companies.Add(c);
            c = new Company() { DisplayName = "Hewlett Packard", Symbol = "HPQ", IsSelected = false };
            Companies.Add(c);
            c = new Company() { DisplayName = "Microsoft", Symbol = "MSFT", IsSelected = false };
            Companies.Add(c);
            c = new Company() { DisplayName = "Oracle", Symbol = "ORCL", IsSelected = false };
            Companies.Add(c);
            c = new Company() { DisplayName = "Google", Symbol = "GOOGL", IsSelected = false };
            Companies.Add(c);
            c = new Company() { DisplayName = "Facebook", Symbol = "FB", IsSelected = false };
            Companies.Add(c);
            c = new Company() { DisplayName = "Twitter", Symbol = "TWTR", IsSelected = false };
            Companies.Add(c);
            c = new Company() { DisplayName = "Intel", Symbol = "INTC", IsSelected = false };
            Companies.Add(c);
            c = new Company() { DisplayName = "AMD", Symbol = "AMD", IsSelected = false };
            Companies.Add(c);
        }
    }
}