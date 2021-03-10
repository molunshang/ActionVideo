using ActionVideo.Models;
using ActionVideo.Services;
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
        private Task loadingTask;
        public ObservableCollection<VideoItem> Items { get; set; }

        public VideosPage(int type, string title)
        {
            InitializeComponent();
            this.type = type;
            Title = title;
            Videos.ItemsSource = Items = new ObservableCollection<VideoItem>();
            loadingTask = LoadVideos();
        }

        Task LoadVideos()
        {
            return api.GetVideoPages(type, page).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    return;
                }
                var videos = t.Result;
                foreach (var item in videos.Items)
                {
                    Items.Add(item);
                }
                total += videos.Items.Count;
                page++;
                if (total >= videos.Total)
                {
                    Videos.Scrolled -= Videos_Scrolled;
                }
            }).ContinueWith(t => loadingTask = null);
        }

        private void Videos_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (loadingTask != null || Items.Count - e.LastVisibleItemIndex > 10)
            {
                return;
            }
            loadingTask = LoadVideos();
        }
    }
}
