
using AzureFoundry.DocAssistant.Core.Ai;
using AzureFoundry.DocAssistant.Core.Ocr;
using Microsoft.Extensions.AI;

namespace AzureFoundry.DocAssistant.Infrastructure.Foundry;

public sealed class FoundryDocumentOcrService(
    IChatClientFactory factory)
    : IDocumentOcrService
{
    internal const string Instructions = """
        You are an OCR engine for Indonesian and English documents
        (receipts, invoices, letters, and forms).

        Your task is to transcribe ALL visible text from the image as accurately as possible.

        Rules:
        - Preserve the reading order and line breaks as much as possible.
          For tables, output one line per table row, with columns separated by " | ".
        - Do not add comments, explanations, or summaries.
          Do not use code blocks.
        - Do not guess unreadable text; write [unreadable] for those sections.
        - If the image contains no text, respond exactly with: [no text]
        """;

    private IChatClient? _client;

    public async Task<string> ReadTextAsync(
        byte[] image,
        string mediaType,
        CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(image);
        ArgumentException.ThrowIfNullOrWhiteSpace(mediaType);

        try
        {
          
            _client ??= factory.Create();

            List<ChatMessage> messages =
            [
                new(ChatRole.System, Instructions),
                new(
                    ChatRole.User,
                    [
                        new TextContent("Read all text from this document."),
                        new DataContent(image, mediaType)
                    ])
            ];

            var response = await _client.GetResponseAsync(
                messages,
                cancellationToken: ct);

            var text = response.Text.Trim();

          
            return text.Length > 0
                ? text
                : throw new OcrException(
                    "The model did not return any text. Please try again.");
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (OcrException)
        {
            throw;
        }
        catch (AiException ex)
        {
            throw new OcrException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new OcrException(
                factory.GetErrorMessage(ex),
                ex);
        }
    }
}
