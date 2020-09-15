using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LatihanWebApi.Data;
using LatihanWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LatihanWebApi.Controllers
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
            //var result = resultRepo.Select(itm => new CategoryDto()
            //    {
            //        Id = itm.CategoryID, 
            //        Nama = itm.NamaCategory}).ToList();

            //  return new JsonResult(result);

        }

        //[HttpGet("{categoryId}")]
        //public async Task<IActionResult> GetCategory(int categoryId)
        //{
        //    var result = await _categoryRepository.GetById(categoryId);
        //    return new JsonResult(result);
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
            // var result = new CategoryDto {Id = resultRepo.CategoryID, Nama = resultRepo.NamaCategory};

        }
    }
}
