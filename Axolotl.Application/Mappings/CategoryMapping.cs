using AutoMapper;
using Axolotl.Application.UseCases.Category.Commands.CreateCommand;
using Axolotl.Application.UseCases.Category.Commands.UpdateCommand;
using Axolotl.Application.Commons.Select.Response;
using Axolotl.Application.Dtos.Category.Response;
using Axolotl.Domain.Entities;
using Axolotl.Utilities.Static;

namespace Axolotl.Application.Mappings;

    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            // The mapping for category
            CreateMap<Category, CategoryResponseDto>()
            .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.UUID))
            .ReverseMap();

            //Now from req to entity
            CreateMap<Category, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.UUID))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
            .ReverseMap();

            CreateMap<Category, CategoryByIdResponseDto>()
            .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.UUID))
            .ReverseMap();

            CreateMap<CreateCategoryCommand, Category>();

            CreateMap<UpdateCategoryCommand, Category>();


        }
        
    }
