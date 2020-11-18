using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIVersioning.Entity
{
    public class Category
    {
       
        public int CategoryID { get; set; }
       
        public string NamaCategory { get; set; }
        public List<Book> Books { get; set; }
    }
}
