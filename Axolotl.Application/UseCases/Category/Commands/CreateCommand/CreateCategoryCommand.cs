using MediatR;
using Axolotl.Application.Commons.Bases;

namespace Axolotl.Application.UseCases.Category.Commands.CreateCommand;

public class CreateCategoryCommand : IRequest<BaseResponse<bool>>
{
    public string Name {get; set;} = null!;
    public string? Description {get; set;}
}
