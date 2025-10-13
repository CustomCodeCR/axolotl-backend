using AutoMapper;
using Axolotl.Application.UseCases.Categories.Commands.CreateCommand;
using Axolotl.Application.UseCases.Categories.Commands.UpdateCommand;
using Axolotl.Application.Commons.Select.Response;
using Axolotl.Application.Dtos.Categories.Response;
using Axolotl.Domain.Entities;
using Axolotl.Utilities.Static;

namespace Axolotl.Application.Mappings;

    public class CategoriesMapping : Profile
    {
        public CategoriesMapping()
        {
            // The mapping for category
            CreateMap<Categories, CategoriesResponseDto>()
            .ForMember(x => x.CategoriesId, x => x.MapFrom(y => y.UUID))
            .ReverseMap();

            //Now from req to entity
            CreateMap<Categories, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.UUID))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
            .ReverseMap();

            CreateMap<Categories, CategoriesByIdResponseDto>()
            .ForMember(x => x.CategoriesId, x => x.MapFrom(y => y.UUID))
            .ReverseMap();

            CreateMap<CreateCategoriesCommand, Categories>();

            CreateMap<UpdateCategoriesCommand, Categories>();


        }
        
    }
