using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LatihanOpenApi.Models
{
   
    public class CategoryDto
    {
        /// <summary>
        /// Category ID
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Nama Kategori
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "Nama tidak boleh lebih dari 50 karakter")]
        public string Nama { get; set; }

        /// <summary>
        /// Daftar Buku berdasarkan Kategori
        /// </summary>
        public ICollection<BookDto> Books { get; set; }= new List<BookDto>();
    }


    /// <summary>
    /// Model untuk proses CRUD pada API
    /// </summary>
    public class KategoriAddDTO
    {
        /// <summary>
        /// Category ID
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Nama Kategori
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "Nama tidak boleh lebih dari 50 karakter")]
        public string Nama { get; set; }

       
    }
}
