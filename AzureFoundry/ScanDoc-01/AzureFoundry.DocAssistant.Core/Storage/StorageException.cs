namespace AzureFoundry.DocAssistant.Core.Storage;

public sealed class StorageException(
    string message,
    Exception? inner = null)
    : Exception(message, inner);
