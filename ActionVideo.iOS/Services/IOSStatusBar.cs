using ActionVideo.Droid.Services;
using ActionVideo.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSStatusBar))]
namespace ActionVideo.Droid.Services
{
    public class IOSStatusBar : IStatusBar
    {
        public void Hide()
        {
            UIApplication.SharedApplication.StatusBarHidden = true;
        }

        public void Show()
        {
            UIApplication.SharedApplication.StatusBarHidden = false;
        }
    }
}