using ChatbotAI.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public interface IChatService
{
    Task<string> SendMessageAsync(List<ChatMessage> messages);
}


