using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotHistory.Models
{
    public class ChatMessage
    {
        public string Role { get; set; } 
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
       
    }
}
