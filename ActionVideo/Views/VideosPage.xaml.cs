using ActionVideo.Models;
using ActionVideo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActionVideo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideosPage : ContentPage
    {
        private readonly VideoApi api = DependencyService.Get<VideoApi>();
        private readonly int type;
        private int page = 1;
        private int total;
        private bool loading = true;

        private Func<Task<IList<VideoItem>>> loadFunc;
        public ObservableCollection<VideoItem> Items { get; set; }

        public VideosPage(int type, string title, bool search)
        {
            InitializeComponent();
            this.type = type;
            Title = title;
            Videos.ItemsSource = Items = new ObservableCollection<VideoItem>();
            if (search)
            {
                loadFunc = SearchVideos;
            }
            else
            {
                loadFunc = LoadVideos;
                Videos.Scrolled += Videos_Scrolled;
            }
            loadFunc().ContinueWith(LoadComplete);
        }

        Task<IList<VideoItem>> LoadVideos()
        {
            return api.GetVideoPages(type, page).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    return null;
                }
                var videos = t.Result;
                total += videos.Items.Count;
                page++;
                if (total >= videos.Total)
                {
                    Videos.Scrolled -= Videos_Scrolled;
                }
                return videos.Items;
            });
        }

        Task<IList<VideoItem>> SearchVideos()
        {
            return api.SearchVideos(Title, page);
        }

        void LoadComplete(Task<IList<VideoItem>> task)
        {
            if (task.IsFaulted || task.Result == null)
            {
                loading = false;
                return;
            }
            lock (Items)
            {
                foreach (var item in task.Result)
                {
                    Items.Add(item);
                }
            }
            loading = false;
        }
        private void Videos_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (loading || Items.Count - e.LastVisibleItemIndex > 10)
            {
                return;
            }
            loading = true;
            loadFunc().ContinueWith(LoadComplete);
        }
    }
}
