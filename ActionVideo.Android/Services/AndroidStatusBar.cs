using ActionVideo.Droid.Services;
using ActionVideo.Services;
using Android.App;
using Android.Views;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidStatusBar))]
namespace ActionVideo.Droid.Services
{
    public class AndroidStatusBar : IStatusBar
    {
        WindowManagerFlags _originalFlags;
        public void Hide()
        {
            var activity = CrossCurrentActivity.Current.Activity;
            var attrs = activity.Window.Attributes;
            _originalFlags = attrs.Flags;
            attrs.Flags |= WindowManagerFlags.Fullscreen;
            activity.Window.Attributes = attrs;
        }

        public void Show()
        {
            var activity = CrossCurrentActivity.Current.Activity;
            var attrs = activity.Window.Attributes;
            attrs.Flags = _originalFlags;
            activity.Window.Attributes = attrs;
        }
    }
}