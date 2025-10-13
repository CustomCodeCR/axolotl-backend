using MediatR;
using Axolotl.Application.Commons.Bases;
using Axolotl.Domain.Entities;

namespace Axolotl.Application.UseCases.Category.Commands.UpdateCommand;

public class UpdateCategoryCommand : IRequest<BaseResponse<bool>>
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}