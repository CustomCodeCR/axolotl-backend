using MediatR;
using Axolotl.Application.Commons.Bases;
using Axolotl.Domain.Entities;

namespace Axolotl.Application.UseCases.Categories.Commands.UpdateCommand;

public class UpdateCategoriesCommand : IRequest<BaseResponse<bool>>
{
    public Guid CategoriesId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}