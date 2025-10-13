using MediatR;
using Axolotl.Application.Commons.Bases;
using Axolotl.Application.Commons.Select.Response;

namespace Axolotl.Application.UseCases.Categories.Queries.GetSelectQuery;

public class GetSelectCategoriesQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}