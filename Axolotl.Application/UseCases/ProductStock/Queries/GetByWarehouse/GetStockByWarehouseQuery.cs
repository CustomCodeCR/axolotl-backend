using Axolotl.Application.Dtos.ProductPriceHistory.Requests;
using MediatR;

namespace Axolotl.Application.UseCases.ProductStock.Queries.GetByWarehouse
{
    public sealed record GetStockByWarehouseQuery(Guid WarehouseId)
        : IRequest<List<ProductStockResponse>>;
}
