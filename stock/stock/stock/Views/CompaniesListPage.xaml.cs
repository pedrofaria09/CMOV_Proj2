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
        TimeSpan timeSpan;

        public CompaniesListPage()
        {
            InitializeComponent();


            BindingContext = viewModel = new CompaniesListViewModel();
        }

        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            Recalculate();
        }

        void OnSwitchToggled(object sender, ToggledEventArgs args)
        {
            Recalculate();
        }

        void Recalculate()
        {
            timeSpan = DateTime.Now - startDatePicker.Date;
            Debug.WriteLine("DATA dif:" + timeSpan.Days);
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem is Company company)
            {
                Company = company;
                if (!company.IsSelected)
                {
                    company.IsSelected = true;
                    counter++;
                }
                else
                {
                    company.IsSelected = false;
                    counter--;
                }
            }
            ((ListView)sender).SelectedItem = null;
        }

        async void GenerateMap(object sender, EventArgs e)
        {
            if(counter < 1 || counter > 2)
                DependencyService.Get<IMessage>().ShortAlert("Choose 1 or 2 Companies");
            else if (timeSpan.Days < 7 || timeSpan.Days > 30)
                DependencyService.Get<IMessage>().ShortAlert("Choose between 7 and 30 days");
            else
                await Navigation.PushModalAsync(new NavigationPage(new HistoryPage(Company)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //viewModel.LoadCitiesCommand.Execute(null);
        }
    }
}