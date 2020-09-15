using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilteringWebApi.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Judul { get; set; }
        public string Penulis { get; set; }
        public string Penerbit { get; set; }
        public string Deskripsi { get; set; }
        public bool Status { get; set; }
        public string Gambar { get; set; }
        public int CategoryId { get; set; }
    }
}
