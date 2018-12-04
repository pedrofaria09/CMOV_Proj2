using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using stock.Models;
using stock.Views;
using stock.ViewModels;

using System.Diagnostics;

namespace stock.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CompaniesListPage : ContentPage
    {
        CompaniesListViewModel viewModel;
        Company Company;
        int counter = 0;
        List<Company> CompaniesSelected = new List<Company>();

        public CompaniesListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new CompaniesListViewModel();
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Debug.WriteLine("TESTE" + ((ListView)sender).SelectedItem);
            if (args.SelectedItem is Company company)
            {
                Company = company;
                if (!company.IsSelected)
                {
                    company.IsSelected = true;
                    CompaniesSelected.Add(company);
                    counter++;
                }
                else
                {
                    company.IsSelected = false;
                    CompaniesSelected.Remove(company);
                    counter--;
                }
            }
            ((ListView)sender).SelectedItem = null;
        }

        async void GenerateMap(object sender, EventArgs e)
        {
            if(CompaniesSelected.Count>0)
                await Navigation.PushModalAsync(new NavigationPage(new HistoryPage(CompaniesSelected)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //viewModel.LoadCitiesCommand.Execute(null);
        }
    }
}