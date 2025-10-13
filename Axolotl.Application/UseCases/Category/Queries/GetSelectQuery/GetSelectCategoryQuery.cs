using MediatR;
using Axolotl.Application.Commons.Bases;
using Axolotl.Application.Commons.Select.Response;

namespace Axolotl.Application.UseCases.Category.Queries.GetSelectQuery;

public class GetSelectCategoryQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}