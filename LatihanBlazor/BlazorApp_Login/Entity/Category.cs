
using System.Collections.Generic;


namespace BlazorAPP_Login.Entity
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string NamaCategory { get; set; }
        public List<Book> Books { get; set; }
    }
}
