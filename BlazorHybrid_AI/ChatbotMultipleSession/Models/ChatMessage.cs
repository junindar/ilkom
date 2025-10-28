using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotMultipleSession.Models
{
    public class ChatMessage
    {
        public string Role { get; set; } 
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
      
    }
    public class ChatSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "New Chat";
        public List<ChatMessage> Messages { get; set; } = new();
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
