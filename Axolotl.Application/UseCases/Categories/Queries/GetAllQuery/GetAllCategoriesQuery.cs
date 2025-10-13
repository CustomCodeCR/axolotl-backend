using MediatR;
using Axolotl.Application.Commons.Bases;
using Axolotl.Application.Dtos.Categories.Response;

namespace Axolotl.Application.UseCases.Categories.Queries.GetAllQuery;

public class GetAllCategoriesQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<CategoriesResponseDto>>>
{
}