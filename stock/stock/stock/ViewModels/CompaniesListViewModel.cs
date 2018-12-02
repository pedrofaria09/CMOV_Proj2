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
using System.Diagnostics;


namespace stock.ViewModels
{
    public class CompaniesListViewModel : BaseViewModel
    {
        public ObservableCollection<Company> Companies { get; set; }

        public CompaniesListViewModel()
        {
            Title = "Companies";
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
            Company c = new Company() { displayName = "Apple", symbol = "AAPL", IsSelected = false };
            Companies.Add(c);
            c = new Company() { displayName = "IBM", symbol = "IBM", IsSelected = false };
            Companies.Add(c);
            c = new Company() { displayName = "Hewlett Packard", symbol = "HPQ", IsSelected = false };
            Companies.Add(c);
            c = new Company() { displayName = "Microsoft", symbol = "MSFT", IsSelected = false };
            Companies.Add(c);
            c = new Company() { displayName = "Oracle", symbol = "ORCL", IsSelected = false };
            Companies.Add(c);
            c = new Company() { displayName = "Google", symbol = "GOOGL", IsSelected = false };
            Companies.Add(c);
            c = new Company() { displayName = "Facebook", symbol = "FB", IsSelected = false };
            Companies.Add(c);
            c = new Company() { displayName = "Twitter", symbol = "TWTR", IsSelected = false };
            Companies.Add(c);
            c = new Company() { displayName = "Intel", symbol = "INTC", IsSelected = false };
            Companies.Add(c);
            c = new Company() { displayName = "AMD", symbol = "AMD", IsSelected = false };
            Companies.Add(c);
        }

    }
}