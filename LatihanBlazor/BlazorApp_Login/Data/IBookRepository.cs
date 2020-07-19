using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BlazorAPP_Login.Entity;

namespace BlazorAPP_Login.Data
{
    public interface IBookRepository
    {
        Task Insert(Book book);
        Task Update(Book book);
        Task Delete(int bookId);
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookById(int bookId);

        Task<IEnumerable<Book>> GetRandomBooks(string cat);

    }
}
