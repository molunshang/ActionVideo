using ActionVideo.Models;
using ActionVideo.Services;
using MediaManager;
using MediaManager.Library;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActionVideo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayPage : ContentPage
    {
        private IStatusBar statusBar = DependencyService.Get<IStatusBar>();
        private IScreenHandler screen = DependencyService.Get<IScreenHandler>();
        public string PlayUrl { get; private set; }
        public PlayPage(VideoItem video)
        {
            InitializeComponent();
            Title = video.Name;
            PlayUrl = video.PlayUrl;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
           
            statusBar.Hide();
            screen.Landscape();
            DeviceDisplay.KeepScreenOn = true;
            
            CrossMediaManager.Current.Notification.Enabled = false;
            var item = await CrossMediaManager.Current.Extractor.CreateMediaItem(PlayUrl);
            item.MediaType = MediaType.Hls;
            await CrossMediaManager.Current.Play(item);

        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            statusBar.Show();
            screen.Portrait();
            DeviceDisplay.KeepScreenOn = false;
            
            await CrossMediaManager.Current.Pause();
            CrossMediaManager.Current.Dispose();
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            //NavBarIsVisible
        }
    }
}