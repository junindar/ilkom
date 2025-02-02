using Latihan.RCL.Components;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebView;
namespace Latihan.WindowsForm
{
    public partial class Form1 : Form
    {
        public Form1(ServiceProvider serviceProvider)
        {
            InitializeComponent();
            blazorWebView.HostPage = "wwwroot/index.html";
            blazorWebView.Services = serviceProvider;
            blazorWebView.RootComponents.Add(
               new RootComponent("#app", typeof(Routes), null));
        }
    }
}
