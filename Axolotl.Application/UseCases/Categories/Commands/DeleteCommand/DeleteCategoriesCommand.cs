using MediatR;
using Axolotl.Application.Commons.Bases;

namespace Axolotl.Application.UseCases.Categories.Commands.DeleteCommand;

public class DeleteCategoriesCommand : IRequest<BaseResponse<bool>>
{
    public Guid CategoriesId {get; set;}
}