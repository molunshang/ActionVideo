using ActionVideo.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ActionVideo
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.RegisterSingleton(CreateHttpClient());
            DependencyService.RegisterSingleton(new VideoApi());
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Console.WriteLine(s);
                Console.WriteLine(e.ExceptionObject);
            };
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                Console.WriteLine(s);
                Console.WriteLine(e.Exception);
            };
            MainPage = new AppShell();
        }

        static HttpClient CreateHttpClient()
        {
            var client = new HttpClient(new HttpClientHandler() { ClientCertificateOptions = ClientCertificateOption.Automatic });
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36");
            return client;
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
