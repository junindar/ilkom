
namespace AzureFoundry.DocAssistant.Core.Ocr;

public interface IDocumentOcrService
{
       Task<string> ReadTextAsync(
        byte[] image,
        string mediaType,
        CancellationToken ct = default);
}

public sealed class OcrException(
    string message,
    Exception? inner = null)
    : Exception(message, inner);
