using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIVersioning.Models
{
    public class CategoryDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Nama tidak boleh lebih dari 50 karakter")]
        public string Nama { get; set; }
        public ICollection<BookDto> Books { get; set; }= new List<BookDto>();
    }
}
