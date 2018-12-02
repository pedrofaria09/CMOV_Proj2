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
                    counter++;
                    DependencyService.Get<IMessage>().ShortAlert("You selected " + company.displayName + " company");
                    //((ListView)sender).BackgroundColor = Color.Red;
                }
                else
                {
                    company.IsSelected = false;
                    counter--;
                    DependencyService.Get<IMessage>().ShortAlert("You deselected " + company.displayName + " company");
                    //((ListView)sender).BackgroundColor = Color.Default;
                }

                //CompaniesListView.SelectedItem = null;
                //Navigation.PushAsync(new CityWeatherPage(city));
            }
            ((ListView)sender).SelectedItem = null;

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