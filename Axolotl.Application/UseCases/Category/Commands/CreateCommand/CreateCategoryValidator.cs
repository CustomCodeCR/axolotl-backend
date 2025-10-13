using FluentValidation;

namespace Axolotl.Application.UseCases.Category.Commands.CreateCommand;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre no puede estar vacío")
            .NotNull().WithMessage("El nombre no puede ser nulo");
    }
}