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
    //[Route("api/v{version:apiVersion}/categories")]
    [Route("api/categories")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
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
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetCategories()
        {

            var resultRepo = await _categoryRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(resultRepo));


        }

        [HttpGet()]
        [MapToApiVersion("1.1")]
        public async Task<IActionResult> GetCategoriesV_1_1()
        {

            var resultRepo = await _categoryRepository.GetAllIncludeBook();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(resultRepo));


        }

        //[HttpGet()]
        //public async Task<IActionResult> GetCategories()
        //{
        //    throw new Exception("Sample Exeption");
        //    var resultRepo = await _categoryRepository.GetAll();
        //    return Ok(_mapper.Map<IEnumerable<CategoryDto>>(resultRepo));


        //}


        [HttpGet("{categoryId}",Name = "GetCategory")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {

            var resultRepo = await _categoryRepository.GetById(categoryId);
            
            if (resultRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryDto>(resultRepo));
           

        }

        

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryDto category)
        {
            var categoryEntity = _mapper.Map<Category>(category);
            categoryEntity = await _categoryRepository.InsertCategory(categoryEntity);

            var categoryForReturn = _mapper.Map<CategoryDto>(categoryEntity);

            return CreatedAtRoute("GetCategory", new { categoryId = categoryForReturn.Id }, categoryForReturn);

        }

        [HttpPut]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(CategoryDto category)
        {
            var cat = await _categoryRepository.GetById(category.Id);

            if (cat == null)
            {
                return NotFound();
            }
            var categoryEntity = _mapper.Map<Category>(category);
            await _categoryRepository.UpdateCategory(categoryEntity);
            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<CategoryDto>> DeleteCategory(int categoryId)
        {
            var cat = await _categoryRepository.GetById(categoryId);

            if (cat == null)
            {
                return NotFound();
            }
            await _categoryRepository.DeleteCategory(categoryId);

          

            return NoContent();
        }
    }

    [ApiController]
    [Route("api/categorycollections")]
    public class CategoryCollectionsController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryCollectionsController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CategoryDto>>>
            CreateMultiple(IEnumerable<CategoryDto> categoryColl)
        {
            var categoryEntities = _mapper.Map<IEnumerable<Category>>(categoryColl);
            await _categoryRepository.InsertMultipleCategory(categoryEntities);

            return Ok();
        }
    }
}
