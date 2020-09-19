using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilteringWebApi.Data;
using FilteringWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilteringWebApi.Controllers
{
    
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;


        }

        [HttpGet()]
        public async Task<IActionResult> GetCategories()
        {

            var resultRepo = await _categoryRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(resultRepo));


        }

        //[HttpGet()]
        //public async Task<IActionResult> GetCategories()
        //{
        //    throw new Exception("Sample Exeption");
        //    var resultRepo = await _categoryRepository.GetAll();
        //    return Ok(_mapper.Map<IEnumerable<CategoryDto>>(resultRepo));


        //}


        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {

            var resultRepo = await _categoryRepository.GetById(categoryId);
            
            if (resultRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryDto>(resultRepo));
           

        }
    }
}
