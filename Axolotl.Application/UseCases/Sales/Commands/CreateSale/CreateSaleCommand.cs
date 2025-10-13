using MediatR;
using Axolotl.Application.Dtos.Sales.Requests;

namespace Axolotl.Application.UseCases.Sales.Commands.CreateSale
{
    public sealed record CreateSaleCommand(CreateSaleDto Dto) : IRequest<Guid>;
}
