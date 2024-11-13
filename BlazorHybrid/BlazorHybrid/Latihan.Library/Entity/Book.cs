using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latihan.Library.Entity
{
    public class Book
    {
        public int Id { get; set; }
        public string Judul { get; set; }
        public string Penulis { get; set; }
        public string Deskripsi { get; set; }
        public string Penerbit { get; set; }
        public bool Status { get; set; }
        public string Gambar { get; set; }
    }
}
