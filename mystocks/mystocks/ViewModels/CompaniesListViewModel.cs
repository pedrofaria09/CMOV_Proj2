using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using mystocks.Models;
using mystocks.Views;
using System.Net;
using System.IO;
using System.Linq;
using mystocks.Services;

namespace mystocks.ViewModels
{
    public class CompaniesListViewModel : BaseViewModel
    {
        public ObservableCollection<Company> companies { get; set; }

        public CompaniesListViewModel()
        {
            Title = "Companies";
            companies = new ObservableCollection<Company>();

            loadCompanies();


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
            Company c = new Company() { displayName = "Apple", symbol = "AAPL" };
            companies.Add(c);
            c = new Company() { displayName = "IBM", symbol = "IBM" };
            companies.Add(c);
            c = new Company() { displayName = "Hewlett Packard", symbol = "HPQ" };
            companies.Add(c);
            c = new Company() { displayName = "Microsoft", symbol = "MSFT" };
            companies.Add(c);
            c = new Company() { displayName = "Oracle", symbol = "ORCL" };
            companies.Add(c);
            c = new Company() { displayName = "Google", symbol = "GOOGL" };
            companies.Add(c);
            c = new Company() { displayName = "Facebook", symbol = "FB" };
            companies.Add(c);
            c = new Company() { displayName = "Twitter", symbol = "TWTR" };
            companies.Add(c);
            c = new Company() { displayName = "Intel", symbol = "INTC" };
            companies.Add(c);
            c = new Company() { displayName = "AMD", symbol = "AMD" };
            companies.Add(c);
        }

    }
}