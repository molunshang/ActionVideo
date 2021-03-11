using ActionVideo.Models;
using ActionVideo.Services;
using MediaManager;
using MediaManager.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActionVideo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayPage : ContentPage
    {
        private IStatusBar statusBar = DependencyService.Get<IStatusBar>();
        private string playUrl;
        public PlayPage(VideoItem video)
        {
            InitializeComponent();
            Title = video.Name;
            playUrl = video.PlayUrl;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            statusBar.Hide();
            var item = await CrossMediaManager.Current.Extractor.CreateMediaItem(playUrl);
            item.MediaType = MediaType.Hls;
            await CrossMediaManager.Current.Play(item);
            CrossMediaManager.Current.Notification.Enabled = false;
            CrossMediaManager.Current.KeepScreenOn = true;
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            statusBar.Show();
            await CrossMediaManager.Current.Pause();
            CrossMediaManager.Current.Dispose();
        }
    }
}