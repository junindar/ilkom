using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilteringWebApi.Entity;
using FilteringWebApi.Parameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilteringWebApi.Data
{
    public class BookRepository : IBookRepository
    {

        private readonly PustakaDbContext _dbContext;

        public BookRepository(PustakaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Insert(Book book)
        {
            _dbContext.Add(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Book book)
        {
            _dbContext.Update(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int bookId)
        {
            var book = _dbContext.Books.Find(bookId);
            _dbContext.Remove(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
          
            return await _dbContext.Books.Include(b => b.Category).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooks(string penerbit)
        {
            if (string.IsNullOrEmpty(penerbit))
            {
                return await GetAllBooks();
            }

            return await _dbContext.Books.Where(c=>c.Penerbit==penerbit).Include(b => b.Category).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooks(string penerbit,string keyword)
        {
            if (string.IsNullOrEmpty(penerbit) && string.IsNullOrEmpty(keyword))
            {
                return await GetAllBooks();
            }

            var books = _dbContext.Books.AsQueryable();

            if (!string.IsNullOrEmpty(penerbit))
            {
                books = books.Where(c => c.Penerbit==penerbit);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                books = books.Where(c => c.Judul.Contains(keyword) || c.Penulis.Contains(keyword) ||
                                         c.Penerbit.Contains(keyword) || c.Deskripsi.Contains(keyword));
            }

            return books.ToList();

        }

        public async Task<IEnumerable<Book>> GetAllBooks(BooksParameters booksParameters)
        {
            if (string.IsNullOrEmpty(booksParameters.Penerbit) && string.IsNullOrEmpty(booksParameters.Keyword))
            {
                return await GetAllBooks();
            }

            var books = _dbContext.Books.AsQueryable();

            if (!string.IsNullOrEmpty(booksParameters.Penerbit))
            {
                books = books.Where(c => c.Penerbit == booksParameters.Penerbit);
            }

            if (!string.IsNullOrEmpty(booksParameters.Keyword))
            {
                books = books.Where(c => c.Judul.Contains(booksParameters.Keyword) 
                                         || c.Penulis.Contains(booksParameters.Keyword) ||
                                         c.Penerbit.Contains(booksParameters.Keyword) || 
                                         c.Deskripsi.Contains(booksParameters.Keyword));
            }

            return books.ToList();
        }


        public async Task<Book> GetBookById(int bookId)
        {
            var book= await _dbContext.Books.Include(b=>b.Category).Where(c => c.BookID == bookId).FirstOrDefaultAsync();
           
            return book;


        }

       
        public async Task<IEnumerable<Book>> GetAllBooksByCategoryId(int categoryId)
        {
            return await _dbContext.Books.Where(c=>c.CategoryID==categoryId).ToListAsync();
        }


    }
}
