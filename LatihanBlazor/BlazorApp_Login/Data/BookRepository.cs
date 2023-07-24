using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAPP_Login.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlazorAPP_Login.Data
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
            var book= await _dbContext.Books.Include(b=>b.Category).Where(c => c.ID == bookId).FirstOrDefaultAsync();
           
            return book;


        }

        public async Task<IEnumerable<Book>> GetRandomBooks(string cat)
        {
            return await _dbContext.Books.Where(c=>c.Category.Nama==cat)
                .OrderBy(r => Guid.NewGuid()).Include(b => b.Category).ToListAsync();
        }
    }
}
