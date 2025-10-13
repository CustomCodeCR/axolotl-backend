using MediatR;
using Axolotl.Application.Commons.Bases;

namespace Axolotl.Application.UseCases.Category.Commands.DeleteCommand;

public class DeleteCategoryCommand : IRequest<BaseResponse<bool>>
{
    public Guid CategoryId {get; set;}
}