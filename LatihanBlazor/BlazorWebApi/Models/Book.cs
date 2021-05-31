using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace BlazorWebApi.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Judul { get; set; }
        public string Penulis { get; set; }
        public string Deskripsi { get; set; }
        public string Penerbit { get; set; }
        public bool Status { get; set; }
        public string Gambar { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }

    }
}
