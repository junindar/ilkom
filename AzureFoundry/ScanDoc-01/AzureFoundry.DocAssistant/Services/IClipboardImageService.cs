
namespace AzureFoundry.DocAssistant.Services;

public sealed record ClipboardImage(byte[] Bytes, string MediaType, string Name);


public sealed class InvalidImageException(string message) : Exception(message);

public interface IClipboardImageService
{
        Task<ClipboardImage?> GetImageAsync(long maxBytes);
}
