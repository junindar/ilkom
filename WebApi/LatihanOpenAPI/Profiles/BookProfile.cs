using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LatihanOpenApi.Entity;
using LatihanOpenApi.Models;

namespace LatihanOpenApi.AutoMapper
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
