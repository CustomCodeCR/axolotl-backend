using MediatR;
using Axolotl.Application.Commons.Bases;
using Axolotl.Application.Dtos.Category.Response;

namespace Axolotl.Application.UseCases.Category.Queries.GetByIdQuery;

public class GetCategoryByIdQuery : IRequest<BaseResponse<CategoryByIdResponseDto>>
{
    public Guid CategoryId { get; set; }
}