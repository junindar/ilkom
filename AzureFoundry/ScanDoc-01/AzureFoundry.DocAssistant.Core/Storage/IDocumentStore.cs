
namespace AzureFoundry.DocAssistant.Core.Storage;

public interface IDocumentStore
{
   
    Task<Document> CreateAsync(
        string title,
        DocumentType type,
        byte[] file,
        string mediaType,
        CancellationToken ct = default);

   
    Task<Document?> GetAsync(
        string id,
        CancellationToken ct = default);

 
    Task<IReadOnlyList<Document>> ListAsync(
        DocumentType? type,
        int max,
        CancellationToken ct = default);

  
    Task SaveTextAsync(
        string id,
        string text,
        CancellationToken ct = default);

   
    Task<DocumentFile?> GetFileAsync(
        string id,
        CancellationToken ct = default);

  
    Task DeleteAsync(
        string id,
        CancellationToken ct = default);

  
   

}

