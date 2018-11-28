using System;
using WeatherApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CityWeatherPage : TabbedPage
	{
        City city;

		public CityWeatherPage(City city)
		{
			InitializeComponent ();
            this.city = city;
            Title = city.DisplayName;

            CurrentPage = TodayPage;
            PastPage.SetCity(city);
            TodayPage.SetCity(city);
            //ForecastPage.SetCity(city);
        }
    }
}