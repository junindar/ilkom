using AzureFoundry.DocAssistant.Core.Ai;
using AzureFoundry.DocAssistant.Core.Ocr;
using AzureFoundry.DocAssistant.Core.Storage;
using AzureFoundry.DocAssistant.Infrastructure.Foundry;
using AzureFoundry.DocAssistant.Infrastructure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFoundry.DocAssistant.Infrastructure;
public static class InfrastructureServiceCollectionExtensions
{
   
    public static IServiceCollection AddDocAssistantInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FoundryOptions>(configuration.GetSection(FoundryOptions.SectionName));
        services.AddSingleton<IChatClientFactory, FoundryClientFactory>();
        services.AddSingleton<IDocumentOcrService, FoundryDocumentOcrService>();

        services.Configure<StorageOptions>(configuration.GetSection(StorageOptions.SectionName));
        services.AddSingleton<IDocumentStore, SqliteDocumentStore>();
        return services;
    }
}
