using FluentValidation;

namespace Axolotl.Application.UseCases.Categories.Commands.CreateCommand;

public class CreateCategoriesValidator : AbstractValidator<CreateCategoriesCommand>
{
    public CreateCategoriesValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre no puede estar vacío")
            .NotNull().WithMessage("El nombre no puede ser nulo");
    }
}