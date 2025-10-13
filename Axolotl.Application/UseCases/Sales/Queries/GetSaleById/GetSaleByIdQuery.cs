using MediatR;
using Axolotl.Application.Dtos.Sales.Requests;

namespace Axolotl.Application.UseCases.Sales.Queries.GetSaleById
{
    public sealed record GetSaleByIdQuery(Guid SaleId) : IRequest<SaleResponse?>;
}
