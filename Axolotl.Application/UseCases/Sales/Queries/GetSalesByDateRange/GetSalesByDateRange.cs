using MediatR;
using Axolotl.Application.Dtos.Sales.Requests;

namespace Axolotl.Application.UseCases.Sales.Queries.GetSalesByDateRange
{
    public sealed record GetSalesByDateRangeQuery(DateTime From, DateTime To) : IRequest<List<SaleResponse>>;
}
