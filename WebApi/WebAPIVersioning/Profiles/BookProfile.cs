using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebAPIVersioning.Entity;
using WebAPIVersioning.Models;

namespace WebAPIVersioning.AutoMapper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>().ForMember(dest=>dest.Id,opt=>opt.MapFrom(src=>src.BookID)
               
            );

            CreateMap<BookDto, Book>().ForMember(dest => dest.BookID, opt => opt.MapFrom(src => src.Id)

            );
        }
    }

   
}
