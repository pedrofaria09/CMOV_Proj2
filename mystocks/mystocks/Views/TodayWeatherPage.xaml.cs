using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WeatherApp.Models;
using WeatherApp.ViewModels;
using System.Threading.Tasks;

namespace WeatherApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TodayWeatherPage : ContentPage
	{
        TodayWeatherViewModel viewModel;

        public TodayWeatherPage()
        {
            InitializeComponent();
        }

        public void SetCity(City city)
        {
            BindingContext = this.viewModel = new TodayWeatherViewModel(city);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Factory.StartNew(() => { viewModel.LoadWeather(); });
        }
    }
}