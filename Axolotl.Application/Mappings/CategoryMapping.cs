using AutoMapper;
using Axolotl.Domain.Entities;
using Axolotl.Application.Dtos;

namespace Axolotl.Application.Mappings
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            // 
            CreateMap<Category, CategoryResponse>()
            .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Products.Count));

            //Now from req to entity
            CreateMap<CategoryRequest, Category>();


        }
        
    }
}