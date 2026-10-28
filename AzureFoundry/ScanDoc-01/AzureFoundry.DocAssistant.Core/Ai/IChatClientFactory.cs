using Microsoft.Extensions.AI;
namespace AzureFoundry.DocAssistant.Core.Ai;

public interface IChatClientFactory
{
    IChatClient Create();
    string GetErrorMessage(Exception ex);
}
