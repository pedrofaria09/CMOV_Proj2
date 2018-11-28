using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using mystocks.Models;
using mystocks.Views;
using mystocks.ViewModels;

namespace mystocks.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CompaniesListPage : ContentPage
    {
        CompaniesListViewModel viewModel;
       

        public CompaniesListPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CompaniesListViewModel();
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem is Company company)
            {
                CompaniesListView.SelectedItem = null;
                //Navigation.PushAsync(new CityWeatherPage(city));
            }
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new NavigationPage(new AddCityPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadCitiesCommand.Execute(null);
        }
    }
}