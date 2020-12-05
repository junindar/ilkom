using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LatihanOpenApi.Entity;
using LatihanOpenApi.Models;

namespace LatihanOpenApi.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ForMember(dest=>dest.Id,opt=>opt.MapFrom(src=>src.CategoryID))
                .ForMember(dest => dest.Nama, opt => opt.MapFrom(src => src.NamaCategory)
            );

            CreateMap<CategoryDto, Category>().ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NamaCategory, opt => opt.MapFrom(src => src.Nama)
                );

            CreateMap<KategoriAddDTO, Category>().ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NamaCategory, opt => opt.MapFrom(src => src.Nama)
                );

            CreateMap<Category, KategoriAddDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryID))
                .ForMember(dest => dest.Nama, opt => opt.MapFrom(src => src.NamaCategory)
                );
        }
    }

   
}
