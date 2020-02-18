using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor_CRUD.Entity;


namespace Blazor_CRUD.Data
{
  public  interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(int categoryId);
    }

   
}
