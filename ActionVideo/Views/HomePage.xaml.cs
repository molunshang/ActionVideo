using ActionVideo.Models;
using ActionVideo.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        private readonly VideoApi api = DependencyService.Get<VideoApi>();
        public HomePage()
        {
            InitializeComponent();

            Items = new ObservableCollection<VideoItem>();
            Videos.ItemsSource = Items;
            api.GetHomeVideos().ContinueWith(t =>
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
                            await Navigation.PushAsync(new VideosPage(data.TypeId, data.TypeName));
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

        private void SearchHandler_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var handler = sender as SearchHandler;
            if (handler == null)
            {
                return;
            }
            
            Console.WriteLine(handler.TextColor);
        }
    }
}
