using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.SendMessage
{
    public class Photo
    {
        public string FileId { get; set; }

        public string FileName { get; set; }
        public string FileUniqueId { get; set; }
        public string Caption { get; set; }
        public string Url { get; set; }

    }

    public class BookCover
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Caption { get; set; }
        public string Url { get; set; }
    }
}
