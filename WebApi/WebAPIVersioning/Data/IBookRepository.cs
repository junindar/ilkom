using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIVersioning.Entity;
using WebAPIVersioning.Parameters;


namespace WebAPIVersioning.Data
{
    public interface IBookRepository
    {
        Task<Book> Insert(Book book);
        Task Update(Book book);
        Task Delete(int bookId);
        Task<IEnumerable<Book>> GetAllBooks();
        Task<IEnumerable<Book>> GetAllBooks(string penerbit);
        Task<IEnumerable<Book>> GetAllBooks(string penerbit,string keyword);
        Task<IEnumerable<Book>> GetAllBooks(BooksParameters booksParameters);
        Task<Book> GetBookById(int bookId);
        Task<IEnumerable<Book>> GetAllBooksByCategoryId(int categoryId);

    }
}
