using ChatbotHistory.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public interface IChatService
{
    Task<string> SendMessageAsync(List<ChatMessage> messages);
}
public interface IChatHistoryService
{
    Task SaveHistoryAsync(List<ChatMessage> messages);
    Task<List<ChatMessage>> LoadHistoryAsync();
    void ClearHistory();
}

