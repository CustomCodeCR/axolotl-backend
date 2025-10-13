using Axolotl.Application.Dtos.ProductPriceHistory.Requests;
using MediatR;

namespace Axolotl.Application.UseCases.ProductPriceHistory.Queries.GetAllByProduct
{
    public sealed record GetAllByProductQuery(Guid ProductId)
        : IRequest<List<ProductPriceHistoryResponse>>;
}
