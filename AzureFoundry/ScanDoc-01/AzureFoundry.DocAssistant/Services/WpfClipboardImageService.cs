using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AzureFoundry.DocAssistant.Services;

public sealed class WpfClipboardImageService : IClipboardImageService
{
    
    public async Task<ClipboardImage?> GetImageAsync(long maxBytes)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxBytes);

        var dispatcher = Application.Current?.Dispatcher
            ?? throw new InvalidOperationException("The WPF application has not been initialized.");

        return await dispatcher.InvokeAsync(() => GetImage(maxBytes));
    }

    private static ClipboardImage? GetImage(long maxBytes)
    {
        //sintaks detail pada project lampiran
        if (!Clipboard.ContainsImage())
        {
            return null;
        }

        var source = Clipboard.GetImage();
        if (source is null)
        {
            return null;
        }

        using var stream = new MemoryStream();
        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(source));
        encoder.Save(stream);

        if (stream.Length > maxBytes)
        {
            throw new InvalidImageException($"The image size exceeds the limit of {maxBytes} bytes.");
        }

        return new ClipboardImage(stream.ToArray(), "image/png", $"clipboard-{DateTime.Now:yyyyMMdd-HHmmss}.png");
    }
}
