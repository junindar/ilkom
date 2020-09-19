using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilteringWebApi.Entity;
using FilteringWebApi.Parameters;


namespace FilteringWebApi.Data
{
    public interface IBookRepository
    {
        Task Insert(Book book);
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
