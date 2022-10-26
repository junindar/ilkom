using System.ComponentModel.DataAnnotations;

namespace MudBlazor.Input.Data.ViewModel
{
    public class Book
    {
        public int BookID { get; set; }
        [Required]
        public string Judul { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Name length can't be more than 255.")]
        public string Penulis { get; set; }
        [Required]
        public string Penerbit { get; set; }
        public string Deskripsi { get; set; }
        public double Harga { get; set; }
        public int Jumlah { get; set; }
        public bool Status { get; set; }
        public string Gambar { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }

    }
}
