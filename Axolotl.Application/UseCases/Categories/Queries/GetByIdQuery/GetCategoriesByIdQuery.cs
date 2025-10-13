using MediatR;
using Axolotl.Application.Commons.Bases;
using Axolotl.Application.Dtos.Categories.Response;

namespace Axolotl.Application.UseCases.Categories.Queries.GetByIdQuery;

public class GetCategoriesByIdQuery : IRequest<BaseResponse<CategoriesByIdResponseDto>>
{
    public Guid CategoriesId { get; set; }
}