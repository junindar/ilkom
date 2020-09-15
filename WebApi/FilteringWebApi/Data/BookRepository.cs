using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilteringWebApi.Entity;
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
