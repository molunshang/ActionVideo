using ActionVideo.Models;
using ActionVideo.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActionVideo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<VideoItem> Items { get; set; }
        private readonly VideoApi api = DependencyService.Get<VideoApi>();
        public HomePage()
        {
            InitializeComponent();
            Videos.ItemsSource = Items = new ObservableCollection<VideoItem>();
            LoadHomeVideo();
        }

        Task LoadHomeVideo()
        {
            return api.GetHomeVideos().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    return;
                }
                foreach (var item in t.Result)
                {
                    Items.Add(item);
                }
            });
        }

        private async void VideoSearchHandler_QueryConfirmed(SearchHandler sender, string key)
        {
            await Navigation.PushAsync(new VideosPage(-1, key, true));
        }
    }
}
