using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using stock.Models;
using stock.ViewModels;
using System.Threading.Tasks;
using SkiaSharp.Views.Forms;
using SkiaSharp;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

using System.Diagnostics;

namespace stock.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        HistoryViewModel viewModel;
        Company Company;

        public HistoryPage(Company company)
        {
            InitializeComponent();
            this.Company = company;
            BindingContext = this.viewModel = new HistoryViewModel(company);
            viewModel.stockDetails.CollectionChanged += StockDetails_CollectionChanged;
        }

        public void SetCompany(Company company)
        {
            BindingContext = this.viewModel = new HistoryViewModel(company);
            //viewModel.HourlyConditions.CollectionChanged += HourlyConditions_CollectionChanged;
        }

        private void StockDetails_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Debug.WriteLine("tamanho teste" + viewModel.stockDetails.Count + " lol " + viewModel.CanDraw);
            if (viewModel.CanDraw)
            {
                Debug.WriteLine("tamanho teste" + viewModel.stockDetails.Count);
                HistoryGraph.InvalidateSurface();
            }
                
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Factory.StartNew(() => { viewModel.LoadHistory(); });
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();
            Debug.WriteLine("vou desenhar o canvas");
            Debug.WriteLine("tamanho 1" + viewModel.stockDetails.Count);
            var stockDetails = viewModel.stockDetails.ToList();
            Debug.WriteLine("tamanho 2" + stockDetails.Count);
            if (stockDetails != null)
            {
                Debug.WriteLine("não é nulo");
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

                SKPaint scalePaint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = Color.Gray.ToSKColor(),
                    TextSize = 10,
                    //TextAlign = SKTextAlign.Center
                };

                if (stockDetails.Count <= 0)
                {
                    return;
                }

                using (var path = new SKPath())
                {
                    var minValue = stockDetails.OrderBy(x => x.closeValue).First().closeValue-1;// hourlyConditions.OrderBy(x => x.Temperature).First().Temperature - 2;
                    var maxValue = stockDetails.OrderByDescending(x => x.closeValue).First().closeValue+1;
                    var valueDifference = maxValue - minValue;
                    var horScale = 30f;
                    var vertScale = 200f;
                    var realVertScale = vertScale+10;
                    int x0 = 0;//(int)(2 * horScale);
                    int y0 = (int)(realVertScale);//(int)((maxTemp - minTemp + 3) * vertScale);

                    // Draw Horizontal Axis
                    canvas.DrawLine(x0, y0, x0 + 21f * horScale, y0, paint);
                    Debug.WriteLine("tamanho " + stockDetails.Count);

                    // Draw Hour Vertical Line
                    canvas.DrawLine(stockDetails.Count * horScale, realVertScale, stockDetails.Count * horScale, 10, paint);


                    for (int j = 0; j < 5; j++)
                    {
                        canvas.DrawText((minValue + (j * valueDifference) / 4).ToString(), stockDetails.Count * horScale, (vertScale * ((4 - j) / (float)4))+14, scalePaint);
                        Debug.WriteLine("texto " + (minValue + (j * valueDifference) / 4) + " " + (vertScale * ((4 - j) / (float)4)));
                        Debug.WriteLine("lololololol " + (vertScale * ((4 - j) / (float)4)) + " " + ((4 - j) / (float)4));
                        canvas.DrawLine(x0, (vertScale * ((4 - j) / (float)4)) + 10, x0 + 21f * horScale, (vertScale * ((4 - j) / (float)4)) + 10, paint);
                    }


                    for (int i = 0; i < stockDetails.Count; i++)
                    {
                        
                        // Draw Scale 
                        

                        //canvas.DrawText("22,7575", stockDetails.Count * horScale, 150, smallTextPaint);
                        // Draw Hour Value Text 
                        //canvas.DrawText(stockDetails[i].date.ToString(), i * (21f * horScale) / stockDetails.Count, vertScale*2, smallTextPaint);

                        // Draw Condition Image 
                        /* DrawImage(canvas, hourlyConditions[i].Icon,
                             x0 + hourlyConditions[i].Hour * horScale - 32,
                             y0 - ((hourlyConditions[i].Temperature - minTemp) * vertScale) - 80);*/

                        // Draw Temperature Value Text 
                        /*canvas.DrawText(
                            hourlyConditions[i].Temperature.ToString(),
                            x0 + hourlyConditions[i].Hour * horScale,
                            y0 - ((hourlyConditions[i].Temperature - minTemp - 1.5f) * vertScale),
                            textPaint);*/

                        // Draw Temperature Line 
                        if (i == 0) {
                            Debug.WriteLine("desenhei " +y0);
                            path.MoveTo(0, y0); }
                        else
                        {
                            path.LineTo(i * (21f * horScale) / stockDetails.Count, ((stockDetails[i].closeValue - minValue) * vertScale / valueDifference)+(realVertScale-vertScale));

                            Debug.WriteLine("desenhei " + ((stockDetails[i].closeValue - stockDetails[i - 1].closeValue) * vertScale / 4));
                        }
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