using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MudBlazor.Table.Entity
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string NamaCategory { get; set; }
        public List<Book> Books { get; set; }
    }
}
