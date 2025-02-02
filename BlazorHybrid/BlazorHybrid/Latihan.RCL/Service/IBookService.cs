using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Latihan.RCL.Entity;

namespace Latihan.RCL.Service
{
    public interface IBookService
    {
      Task<List<Book>?> GetBooksAsync();
      Task<Book?> GetBookByIdAsync(int id);

    }
}
