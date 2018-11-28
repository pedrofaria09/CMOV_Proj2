using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WeatherApp.Models;
using WeatherApp.ViewModels;
using System.Threading.Tasks;
using SkiaSharp.Views.Forms;
using SkiaSharp;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PastWeatherPage : ContentPage
    {
        PastWeatherViewModel viewModel;

        public PastWeatherPage()
        {
            InitializeComponent();
        }

        public void SetCity(City city)
        {
            BindingContext = this.viewModel = new PastWeatherViewModel(city);
            viewModel.HourlyConditions.CollectionChanged += HourlyConditions_CollectionChanged;
        }

        private void HourlyConditions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (viewModel.HourlyConditions.Count == 0 || viewModel.HourlyConditions.Count == 8)
                temperatureGraph.InvalidateSurface();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Factory.StartNew(() => { viewModel.LoadWeather(); });
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();
            var hourlyConditions = viewModel.HourlyConditions.ToList();
            if (hourlyConditions != null && hourlyConditions.Count == 8)
            {
                SKPaint paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = Color.Gray.ToSKColor(),
                    StrokeWidth = 1
                };

                SKPaint textPaint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = Color.Black.ToSKColor(),
                    TextSize = 30,
                    TextAlign = SKTextAlign.Center
                };

                SKPaint smallTextPaint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = Color.Gray.ToSKColor(),
                    TextSize = 20,
                    TextAlign = SKTextAlign.Center
                };

                using (var path = new SKPath())
                {
                    var minTemp = hourlyConditions.OrderBy(x => x.Temperature).First().Temperature - 2;
                    var maxTemp = hourlyConditions.OrderByDescending(x => x.Temperature).First().Temperature + 2;
                    var horScale = 30f;
                    var vertScale = 20f;                    
                    int x0 = (int)(2 * horScale);
                    int y0 = (int)((maxTemp - minTemp + 3) * vertScale);

                    /* Draw Horizontal Axis */
                    canvas.DrawLine(x0, y0, x0 + 21f * horScale, y0, paint);

                    for (int i = 0; i < hourlyConditions.Count; i++)
                    {
                        /* Draw Hour Vertical Line */
                        canvas.DrawLine(x0 + hourlyConditions[i].Hour * horScale, y0, x0 + hourlyConditions[i].Hour * horScale, y0 - ((maxTemp - minTemp) * vertScale), paint);

                        /* Draw Hour Value Text */
                        canvas.DrawText(hourlyConditions[i].Hour.ToString() + "h", x0 + hourlyConditions[i].Hour * horScale, y0 + vertScale, smallTextPaint);

                        /* Draw Condition Image */
                        DrawImage(canvas, hourlyConditions[i].Icon,
                            x0 + hourlyConditions[i].Hour * horScale - 32,
                            y0 - ((hourlyConditions[i].Temperature - minTemp) * vertScale) - 80);

                        /* Draw Temperature Value Text */
                        canvas.DrawText(
                            hourlyConditions[i].Temperature.ToString(),
                            x0 + hourlyConditions[i].Hour * horScale,
                            y0 - ((hourlyConditions[i].Temperature - minTemp - 1.5f) * vertScale),
                            textPaint);
                        
                        /* Draw Temperature Line */
                        if (i == 0)
                            path.MoveTo(x0, y0 - ((hourlyConditions[i].Temperature - minTemp) * vertScale));
                        else
                            path.LineTo(x0 + hourlyConditions[i].Hour * horScale, y0 - ((hourlyConditions[i].Temperature - minTemp) * vertScale));
                    }

                    paint.Color = Color.Blue.ToSKColor();
                    paint.StrokeWidth = 2;
                    canvas.DrawPath(path, paint);
                }
            }
        }

        void DrawImage(SKCanvas canvas, string icon, float x, float y)
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            string resourceID = assembly.GetName().Name + "." + icon.Replace("//cdn.apixu.com/", "").Replace("/", ".").Replace("64x64", "_64x64");

            using (Stream stream = assembly.GetManifestResourceStream(resourceID))
            {
                if (stream != null)
                {
                    using (SKManagedStream skStream = new SKManagedStream(stream))
                    {
                        var resourceBitmap = SKBitmap.Decode(skStream);
                        if (x < 0) x = 0;
                        if (y < 0) y = 0;
                        canvas.DrawBitmap(resourceBitmap, x, y);
                    }
                }
            }
        }
    }
}