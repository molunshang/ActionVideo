using ActionVideo.Droid.Services;
using ActionVideo.Services;
using Android.Content.PM;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(ScreenHandler))]
namespace ActionVideo.Droid.Services
{
    public class ScreenHandler : IScreenHandler
    {
        public void Landscape()
        {
            CrossCurrentActivity.Current.Activity.RequestedOrientation = ScreenOrientation.Landscape;
        }

        public void Portrait()
        {
            CrossCurrentActivity.Current.Activity.RequestedOrientation = ScreenOrientation.Portrait;
        }
    }
}