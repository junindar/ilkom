using System.Configuration;
using System.Data;
using System.Windows;
using Latihan.RCL.Service;
using Latihan.WPF.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Latihan.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_OnStartup(object sender, StartupEventArgs e)
        {
            var services = new ServiceCollection();
            services.AddWpfBlazorWebView();
#if DEBUG
            services.AddBlazorWebViewDeveloperTools();
#endif
           
            services.AddAuthorizationCore();
         
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            services.AddTransient<IBookService, BookService>();
            var serviceProvider = services.BuildServiceProvider();
            this.Resources.Add("ServiceProvider", serviceProvider);

        }

    }
}