namespace AzureFoundry.DocAssistant.Infrastructure.Storage;

public sealed class StorageOptions
{
    public const string SectionName = "Storage";

   
    public string Folder { get; set; } = "";
}
