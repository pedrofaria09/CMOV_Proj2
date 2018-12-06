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

            Companies = new ObservableCollection<Company>();

            loadCompanies();
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