using System.Windows;
using Client.UI.Windows;
using Client.Utils.Runners;
using Client.Utils.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private async void App_OnStartup(object sender, StartupEventArgs e)
        {
            var builder = Host.CreateApplicationBuilder();

            // Add Services
            builder.Services.AddSingleton<RestService>();

            // Add Windows
            builder.Services.AddSingleton<LoginWindow>();
            builder.Services.AddSingleton<MainWindow>();
            builder.Services.AddSingleton<RegisterWindow>();

            builder.Services.AddHostedService<WindowRunner>();

            var app = builder.Build();
            await app.RunAsync();
        }
    }
}