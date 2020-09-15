using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilteringWebApi.Entity;
using FilteringWebApi.Models;

namespace FilteringWebApi.AutoMapper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>().ForMember(dest=>dest.Id,opt=>opt.MapFrom(src=>src.BookID)
               
            );
        }
    }

   
}
