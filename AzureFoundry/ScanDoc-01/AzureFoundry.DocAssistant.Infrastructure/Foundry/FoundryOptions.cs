namespace AzureFoundry.DocAssistant.Infrastructure.Foundry;
public sealed class FoundryOptions
    {
        public const string SectionName = "Foundry";

       
        public string Endpoint { get; set; } = "";

        
        public string ApiKey { get; set; } = "";


        public string ModelDeployment { get; set; } = "";
    }

