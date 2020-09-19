using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilteringWebApi.Data;
using FilteringWebApi.Models;
using FilteringWebApi.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace FilteringWebApi.Controllers
{
    
    [ApiController]
   public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public BookController(IBookRepository bookRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;


        }

        //[HttpGet()]
        //[Route("api/books")]
        //public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks(string penerbit)
        //{

        //    var resultRepo = await _bookRepository.GetAllBooks(penerbit);
        //    return Ok(_mapper.Map<IEnumerable<BookDto>>(resultRepo));
        //}

        [HttpGet()]
        [Route("api/books")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks([FromQuery] BooksParameters booksParameters)
        {

            var resultRepo = await _bookRepository.GetAllBooks(booksParameters);
            return Ok(_mapper.Map<IEnumerable<BookDto>>(resultRepo));
        }


        [HttpGet()]
        [Route("api/categories/{categoryId}/books")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByCategory(int categoryId)
        {


            var cat = await _categoryRepository.GetById(categoryId);

            if (cat == null)
            {
                return NotFound();
            }
            var resultRepo = await _bookRepository.GetAllBooksByCategoryId(categoryId);
            return Ok(_mapper.Map<IEnumerable<BookDto>>(resultRepo));


        }

        [Route("api/categories/{categoryId}/books/{bookid}")]
        public async Task<ActionResult<BookDto>> GetBookByCategory(int categoryId,int bookid)
        {

            var cat = await _categoryRepository.GetById(categoryId);

            if (cat == null)
            {
                return NotFound();
            }
            var resultRepo = await _bookRepository.GetBookById(bookid);
            if (resultRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookDto>(resultRepo));

        }
    }
}
