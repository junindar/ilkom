using AzureFoundry.DocAssistant.Core.Storage;

namespace AzureFoundry.DocAssistant.Core.Ocr;

public sealed class DocumentScanService(
    IDocumentStore store,
    IDocumentOcrService ocr)
{
   
    public Task<Document> SaveAsync(
        byte[] bytes,
        string mediaType,
        string name,
        CancellationToken ct = default) =>
        store.CreateAsync(name, DocumentType.Image, bytes, mediaType, ct);

        public async Task<Document> ReadAsync(
        string id,
        CancellationToken ct = default)
    {
        var document = await store.GetAsync(id, ct)
            ?? throw new OcrException($"Document '{id}' was not found.");

        if (document.Status == DocumentStatus.Ready)
        {
            return document;
        }

        var file = await store.GetFileAsync(id, ct)
            ?? throw new OcrException(
                "The document image file was not found in storage.");

        var text = await ocr.ReadTextAsync(
            file.Bytes,
            file.MediaType,
            ct);

        await store.SaveTextAsync(id, text, ct);

        return document with
        {
            Text = text,
            Status = DocumentStatus.Ready
        };
    }
}
