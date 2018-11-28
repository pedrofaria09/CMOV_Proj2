using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCityPage : ContentPage
    {
        private AddCityViewModel _viewModel;

        public AddCityPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new AddCityViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as City;
            if (item == null)
                return;

            MessagingCenter.Send(this, "AddCity", _viewModel.SelectedCity);
            CitiesListView.SelectedItem = null;
            await Navigation.PopModalAsync();
        }
    }
}