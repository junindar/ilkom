using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilteringWebApi.Entity;
using FilteringWebApi.Models;

namespace FilteringWebApi.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ForMember(dest=>dest.Id,opt=>opt.MapFrom(src=>src.CategoryID))
                .ForMember(dest => dest.Nama, opt => opt.MapFrom(src => src.NamaCategory)
            );
        }
    }

   
}
