using MediatR;
using Axolotl.Application.Commons.Bases;
using Axolotl.Application.Dtos.Category.Response;

namespace Axolotl.Application.UseCases.Category.Queries.GetAllQuery;

public class GetAllCategoryQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<CategoryResponseDto>>>
{
}