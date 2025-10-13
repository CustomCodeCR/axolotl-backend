using MediatR;
using Axolotl.Application.Commons.Bases;

namespace Axolotl.Application.UseCases.Categories.Commands.CreateCommand;

public class CreateCategoriesCommand : IRequest<BaseResponse<bool>>
{
    public string Name {get; set;} = null!;
    public string? Description {get; set;}
}
