using ChatbotMultipleSession.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatbotMultipleSession.Service
{
    public class ChatSessionService: IChatSessionService
    {
        private const string FileName = "chat_history.json";
        private readonly string _filePath;
        public List<ChatSession> Sessions { get; private set; } = new();

        public ChatSession? CurrentSession { get; private set; }

        public ChatSessionService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, FileName);
            LoadSessions();
        }
        public void CreateNewSession(string? title = null)
        {
            var session = new ChatSession
            {
                Title = title ?? $"Chat {Sessions.Count + 1}"
            };
            Sessions.Add(session);
            CurrentSession = session;
            SaveSessions();
        }

        public void SwitchSession(Guid id)
        {
            CurrentSession = Sessions.FirstOrDefault(s => s.Id == id);
        }

        public void AddMessage(string role, string content)
        {
            if (CurrentSession == null) return;

            CurrentSession.Messages.Add(new ChatMessage
            {
                Role = role,
                Content = content
            });

            CurrentSession.LastUpdated = DateTime.Now;
            SaveSessions();
        }

        public void DeleteSession(Guid id)
        {
            var target = Sessions.FirstOrDefault(s => s.Id == id);
            if (target != null)
            {
                Sessions.Remove(target);
                if (CurrentSession?.Id == id)
                    CurrentSession = Sessions.FirstOrDefault();
                SaveSessions();
            }
        }

        public void SaveSessions()
        {
            try
            {
                var json = JsonSerializer.Serialize(Sessions, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch { /* handle error */ }
        }

        public void LoadSessions()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var json = File.ReadAllText(_filePath);
                    Sessions = JsonSerializer.Deserialize<List<ChatSession>>(json) ?? new();
                    CurrentSession = Sessions.FirstOrDefault();
                }
            }
            catch
            {
                Sessions = new();
            }
        }

        public void RenameSession(Guid id, string newTitle)
        {
            var target = Sessions.FirstOrDefault(s => s.Id == id);
            if (target != null && !string.IsNullOrWhiteSpace(newTitle))
            {
                target.Title = newTitle.Trim();
                target.LastUpdated = DateTime.Now;
                SaveSessions();
            }
        }

    }
}
