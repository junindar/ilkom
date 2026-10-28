namespace AzureFoundry.DocAssistant.Core.Storage;

public enum DocumentType
{
    Image = 0,
}

public enum DocumentStatus
{
    Unread = 0,
    Ready = 1
   
}

public sealed record Document(
    string Id,
    string Title,
    DocumentType Type,
    DateTime Time,
    DocumentStatus Status,
    string? Text);

public sealed record DocumentFile(
    byte[] Bytes,
    string MediaType);

