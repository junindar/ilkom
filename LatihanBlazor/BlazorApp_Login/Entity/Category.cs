
using System.Collections.Generic;


namespace BlazorAPP_Login.Entity
{
    public class Category
    {
        public int ID { get; set; }
        public string Nama { get; set; }
        public List<Book> Books { get; set; }
    }
}
