using Latihan.RCL.Service;
using Latihan.WindowsForm.Auth;
using Microsoft.AspNetCore.Components.Authorization;

using Microsoft.Extensions.DependencyInjection;
namespace Latihan.WindowsForm
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
#if DEBUG
            services.AddBlazorWebViewDeveloperTools();
#endif

            services.AddAuthorizationCore();

            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            services.AddTransient<IBookService, BookService>();
            var serviceProvider = services.BuildServiceProvider();
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(serviceProvider));
        }
    }
}