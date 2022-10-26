using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MudBlazor.Input.Data.ViewModel;

namespace MudBlazor.Input.Data
{
    public class CategoryRepository: ICategoryRepository
    {
        List<Category> Categories;
        public CategoryRepository()
        {
            Categories = new List<Category>
                {
                    new Category { NamaCategory = "Agama",CategoryID=1 },
                    new Category { NamaCategory = "Bahasa",CategoryID=2 },
                     new Category { NamaCategory = "Teknologi" ,CategoryID=3},
                      new Category { NamaCategory = "Sosial",CategoryID=4 },
                };
           

        }
        public async Task<List<Category>> GetAll()
        {
            await Task.Delay(2);
            return Categories;
        }

        public async Task<Category> GetById(int categoryId)
        {
            await Task.Delay(2);
            return Categories.First(c => c.CategoryID == categoryId);
        }
    }
}
