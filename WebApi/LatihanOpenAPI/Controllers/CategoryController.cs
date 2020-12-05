using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LatihanOpenApi.Data;
using LatihanOpenApi.Entity;
using LatihanOpenApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LatihanOpenApi.Controllers
{
    
    [ApiController]
    [Route("api/categories")]
    //Category Controller
    [ApiExplorerSettings(GroupName = "LatihanOpenAPICategory")]
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

       



        /// <summary>
        /// Untuk mendapatkan Category berdasarkan Category ID
        /// </summary>
        /// <param name="categoryId">ID yang dicari</param>
        /// <returns>Jika ID yang dicari ditemukan, maka akan mendapatkan data category dan daftar buku</returns>
        /// <remarks>
        /// 
        /// </remarks>
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


        /// <summary>
        /// Tambah Kategori
        /// </summary>
        /// <returns>Data Kategori yang baru dibuat</returns>
        /// <remarks>
        /// Contoh untuk menambah kategori \
        /// {\
        ///     "Id": 0,\
        ///     "Nama" : "Contoh Kategori"\
        /// }
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<KategoriAddDTO>> CreateCategory(KategoriAddDTO category)
        {
            var categoryEntity = _mapper.Map<Category>(category);
            categoryEntity = await _categoryRepository.InsertCategory(categoryEntity);

            var categoryForReturn = _mapper.Map<KategoriAddDTO>(categoryEntity);

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
  [ApiExplorerSettings(GroupName = "LatihanOpenAPICategory")]
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
