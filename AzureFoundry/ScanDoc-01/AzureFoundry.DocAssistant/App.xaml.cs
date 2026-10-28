using System.Windows;
using AzureFoundry.DocAssistant.Core;
using AzureFoundry.DocAssistant.Infrastructure;
using AzureFoundry.DocAssistant.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFoundry.DocAssistant;

public partial class App : Application
{
    private ServiceProvider? _serviceProvider;
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Urutan: appsettings.json (default kosong) -> user-secrets (nilai asli) -> environment variable.
        // Nilai yang belakangan menimpa yang sebelumnya.
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets<App>(optional: true)
            .AddEnvironmentVariables(prefix: "DOCASSISTANT_")
            .Build();

        var services = new ServiceCollection();
        services.AddWpfBlazorWebView();

#if DEBUG
        services.AddBlazorWebViewDeveloperTools();
#endif

        services.AddDocAssistantCore();
        services.AddDocAssistantInfrastructure(configuration);

     
        services.AddSingleton<IClipboardImageService, WpfClipboardImageService>();

        _serviceProvider = services.BuildServiceProvider();
        Resources["ServiceProvider"] = _serviceProvider;

        new MainWindow().Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _serviceProvider?.Dispose();
        base.OnExit(e);
    }
}
