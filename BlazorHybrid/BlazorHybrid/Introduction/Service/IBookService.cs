using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Introduction.Entity;

namespace Introduction.Service
{
    public interface IBookService
    {
      Task<List<Book>?> GetBooksAsync();
      Task<Book?> GetBookByIdAsync(int id);

    }
}
