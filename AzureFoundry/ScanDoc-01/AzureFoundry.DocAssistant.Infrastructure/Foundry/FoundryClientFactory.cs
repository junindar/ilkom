
using System.ClientModel;
using System.Text.RegularExpressions;
using AzureFoundry.DocAssistant.Core.Ai;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;
using OpenAI;

namespace AzureFoundry.DocAssistant.Infrastructure.Foundry;

public sealed class FoundryClientFactory(
    IOptions<FoundryOptions> options)
    : IChatClientFactory
{
    private readonly FoundryOptions _options = options.Value;

    public IChatClient Create()
    {
        if (string.IsNullOrWhiteSpace(_options.Endpoint) ||
            string.IsNullOrWhiteSpace(_options.ApiKey) ||
            string.IsNullOrWhiteSpace(_options.ModelDeployment))
        {
            throw new AiException(
                "Foundry is not configured. Please set Foundry:Endpoint, Foundry:ApiKey, and Foundry:ModelDeployment in user-secrets.");
        }

        var client = new OpenAIClient(
            new ApiKeyCredential(_options.ApiKey),
            new OpenAIClientOptions
            {
                Endpoint = NormalizeEndpoint(_options.Endpoint)
            });

        return client.GetChatClient(_options.ModelDeployment)
            .AsIChatClient();
    }

    public static Uri NormalizeEndpoint(string endpoint)
    {
        var text = endpoint.Trim().TrimEnd('/');

        if (!text.EndsWith("/openai/v1",
                StringComparison.OrdinalIgnoreCase))
        {
            text += "/openai/v1";
        }

        return new Uri(text + "/");
    }

    public string GetErrorMessage(Exception ex)
    {
        var text = ex.ToString();
        // Detail sintaks pada project lampiran
        if (ContainsStatusCode(text, 401) ||
            text.Contains("Unauthorized",
                StringComparison.OrdinalIgnoreCase))
        {
            return "The API key was rejected. Copy Key 1 again from Keys and Endpoint, and ensure that API key authentication has not been disabled by your administrator.";
        }

        if (ContainsStatusCode(text, 403) ||
            text.Contains("PermissionDenied",
                StringComparison.OrdinalIgnoreCase))
        {
            return "Access denied. Check the network restrictions (firewall) on the Foundry resource or your organization's policies.";
        }

        if (ContainsStatusCode(text, 404) ||
            text.Contains("DeploymentNotFound",
                StringComparison.OrdinalIgnoreCase))
        {
            return "The model deployment was not found. Verify Foundry:ModelDeployment (deployment name) and Foundry:Endpoint.";
        }

        if (ContainsStatusCode(text, 429))
        {
            return "The model quota has been exceeded or too many requests have been sent. Please wait a moment and try again.";
        }

        if (text.Contains("HttpRequestException") ||
            text.Contains("No such host",
                StringComparison.OrdinalIgnoreCase))
        {
            return "Unable to reach Foundry. Check your internet connection and the endpoint configuration.";
        }

        return "Failed to connect to Foundry: " + ex.Message;
    }


    private static bool ContainsStatusCode(string text, int code) =>
        Regex.IsMatch(text, $@"(?<!\d){code}(?!\d)");
}
