using Axolotl.Application.Dtos.ProductPriceHistory.Requests;
using MediatR;

namespace Axolotl.Application.UseCases.ProductPriceHistory.Queries.GetCurrentByProduct
{
    public sealed record GetCurrentPriceQuery(Guid ProductId, DateTime At)
        : IRequest<ProductPriceHistoryResponse?>;
}
