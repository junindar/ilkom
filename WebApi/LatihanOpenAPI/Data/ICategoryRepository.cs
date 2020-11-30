using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatihanOpenApi.Entity;


namespace LatihanOpenApi.Data
{
  public  interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(int categoryId);
        Task<Category> InsertCategory(Category obj);
        Task InsertMultipleCategory(IEnumerable<Category> obj);
        Task<Category> UpdateCategory(Category obj);
        Task DeleteCategory(int id);
    }

   
}
