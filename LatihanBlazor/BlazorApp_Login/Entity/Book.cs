


namespace BlazorAPP_Login.Entity
{
    public class Book
    {
        public int BookID { get; set; }
        public string Judul { get; set; }
        public string Penulis { get; set; }
        public string Penerbit { get; set; }
        public string Deskripsi { get; set; }
        public bool Status { get; set; }
        public string Gambar { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }

    }
}
