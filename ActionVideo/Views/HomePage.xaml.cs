using ActionVideo.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActionVideo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<VideoItem> Items { get; set; }
        private readonly HttpClient client = DependencyService.Get<HttpClient>();
        public HomePage()
        {
            InitializeComponent();

            Items = new ObservableCollection<VideoItem>();
            Videos.ItemsSource = Items;
            client.GetStringAsync("https://avninga.com/").ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    return;
                }
                try
                {
                    var match = Regex.Match(t.Result, ">(\\{.+?\\})<");
                    if (!match.Success)
                    {
                        return;
                    }
                    var root = JObject.Parse(match.Groups[1].Value);
                    var list = root["props"]["pageProps"]["homePageData"]["categories"].Children().SelectMany(items =>
                    {
                        return items["vods"].Children().Select(it => new VideoItem()
                        {
                            Pic = it.Value<string>("vod_pic"),
                            Name = it.Value<string>("vod_name"),
                            DateTime = it.Value<string>("vod_time_add").Substring(0, 10),
                            PlayUrl = it.Value<string>("vod_play_url")
                        });
                    });
                    foreach (var it in list)
                    {
                        Items.Add(it);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
