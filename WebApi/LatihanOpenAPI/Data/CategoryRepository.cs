using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatihanOpenApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace LatihanOpenApi.Data
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly PustakaDbContext _dbContext;

        public CategoryRepository(PustakaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Category>> GetAll()
        {
            return await _dbContext.Categories.ToListAsync();
        }
        public async Task<List<Category>> GetAllIncludeBook()
        {
            return await _dbContext.Categories.Include(c => c.Books).ToListAsync();
        }
        public async Task<Category> GetById(int categoryId)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c=>c.CategoryID== categoryId);
        }

        public async Task<Category> InsertCategory(Category obj)
        {
            _dbContext.Add(obj);
            await _dbContext.SaveChangesAsync();
            return obj ;
        }

        public async Task InsertMultipleCategory(IEnumerable<Category> list)
        {
            foreach (var itm in list)
            {
                _dbContext.Add(itm);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Category> UpdateCategory(Category obj)
        {
            _dbContext.Update(obj);
            await _dbContext.SaveChangesAsync();
            return obj;
        }

        public async Task DeleteCategory(int id)
        {
            var book = _dbContext.Categories.Find(id);
            _dbContext.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
    }
}
