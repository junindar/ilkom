using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAPP_Login.Entity;


namespace BlazorAPP_Login.Data
{
  public  interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(int categoryId);
    }

   
}
