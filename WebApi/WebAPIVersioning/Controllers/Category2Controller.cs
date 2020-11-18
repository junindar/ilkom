using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebAPIVersioning.Data;
using WebAPIVersioning.Entity;
using WebAPIVersioning.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIVersioning.Controllers
{
    
    [ApiController]
    [Route("api/v{version:apiVersion}/categories")]
    //[Route("api/categories")]
    [ApiVersion("2.0")]
    public class Category2Controller : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public Category2Controller(ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> GetCategories()
        {
            var resultRepo = await _categoryRepository.GetAll();
            var result = new
            {
                Count=resultRepo.Count,
                Results=_mapper.Map<IEnumerable<CategoryDto>>(resultRepo)
            };
            return Ok(result);
        }

       


        

      
    }

    
}
