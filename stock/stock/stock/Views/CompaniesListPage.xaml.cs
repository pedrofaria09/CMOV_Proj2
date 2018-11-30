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
       

        public CompaniesListPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CompaniesListViewModel();
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem is Company company)
            {
                Company = company;
                //CompaniesListView.SelectedItem = null;
                //Navigation.PushAsync(new CityWeatherPage(city));
            }
        }

        async void GenerateMap(object sender, EventArgs e)
        {
            if(Company!=null)
                await Navigation.PushModalAsync(new NavigationPage(new HistoryPage(Company)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //viewModel.LoadCitiesCommand.Execute(null);
        }
    }
}