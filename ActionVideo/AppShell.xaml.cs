using ActionVideo.ViewModels;
using ActionVideo.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ActionVideo
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            this.Items.Add(new MenuItem() { Text = "国产女奴调教", IsEnabled = true });
            this.Items.Add(new MenuItem() { Text = "福利姬", IsEnabled = true });
            this.Items.Add(new MenuItem() { Text = "JAV高清", IsEnabled = true });
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Current.GoToAsync("//LoginPage");
        }
    }
}
