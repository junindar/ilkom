
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ChatbotMultipleSession.Models;

namespace ChatbotMultipleSession.Service
{
    public interface IChatSessionService
    {

        List<ChatSession> Sessions { get; }
        ChatSession? CurrentSession { get; }
        void CreateNewSession(string? title = null);
        void SwitchSession(Guid id);
        void AddMessage(string role, string content);
        void DeleteSession(Guid id);
        void SaveSessions();
        void LoadSessions();
        void RenameSession(Guid id, string newTitle);
    }
   
}


