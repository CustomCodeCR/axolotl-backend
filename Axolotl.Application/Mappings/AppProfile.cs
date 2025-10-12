using AutoMapper;
using Axolotl.Domain.Entities;
using Axolotl.Application.Dtos;

namespace Axolotl.Application.Mappings
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<Products, ProductsDto>().ReverseMap();
        }
    }
}
