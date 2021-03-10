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
                var categories = t.Result.Item1;
                Device.BeginInvokeOnMainThread(() =>
                {
                    foreach (var category in categories)
                    {
                        var menuItem = new MenuItem() { Text = category.TypeName, BindingContext = category };
                        menuItem.Clicked += async (sender, e) =>
                        {
                            Shell.Current.FlyoutIsPresented = false;
                            var data = (Category)((MenuItem)sender).BindingContext;
                            await Navigation.PushAsync(new VideosPage(data.TypeId, data.TypeName, false));
                        };
                        Shell.Current.Items.Add(menuItem);
                    }
                });
                foreach (var item in t.Result.Item2)
                {
                    Items.Add(item);
                }
            });
        }

        private async void VideoSearchHandler_QueryConfirmed(SearchHandler sender, string key)
        {
            sender.ClearValue(SearchHandler.QueryProperty);
            await Navigation.PushAsync(new VideosPage(-1, key, true));
        }
    }
}
