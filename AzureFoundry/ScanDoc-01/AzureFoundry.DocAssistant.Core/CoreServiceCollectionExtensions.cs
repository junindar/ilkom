using AzureFoundry.DocAssistant.Core.Ocr;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFoundry.DocAssistant.Core;

public static class CoreServiceCollectionExtensions
{
    public static IServiceCollection AddDocAssistantCore(this IServiceCollection services)
    {
        services.AddSingleton<DocumentScanService>();
        return services;
    }
}
