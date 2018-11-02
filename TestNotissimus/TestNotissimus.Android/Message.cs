using Android.App;
using Android.Widget;

using TestNotissimus.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(MessageDroid))]
namespace TestNotissimus.Droid
{
    public class MessageDroid: IMessage
    {
        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }

        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
    }
}