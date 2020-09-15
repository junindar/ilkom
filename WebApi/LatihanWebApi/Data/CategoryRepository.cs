using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatihanWebApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace LatihanWebApi.Data
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

        public async Task<Category> GetById(int categoryId)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c=>c.CategoryID== categoryId);
        }
    }
}
