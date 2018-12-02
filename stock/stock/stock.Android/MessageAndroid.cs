using System;
using Android.Widget;
using stock.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace stock.Droid
{
    public class MessageAndroid : IMessage
    {

        public void LongAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}
