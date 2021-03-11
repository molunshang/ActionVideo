using ActionVideo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActionVideo.Views.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoListView
    {
        public VideoListView()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            var video = (VideoItem)((BindableObject)sender).BindingContext;
            await Navigation.PushAsync(new PlayPage(video));
        }
    }
}