namespace AzureFoundry.DocAssistant.Core.Ai;

public sealed class AiException(string message, Exception? inner = null) : Exception(message, inner);
