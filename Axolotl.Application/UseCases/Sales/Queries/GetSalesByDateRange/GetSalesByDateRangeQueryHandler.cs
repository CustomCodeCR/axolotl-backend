using MediatR;
using Axolotl.Application.Dtos.Sales.Requests;
using Axolotl.Application.Interfaces.Services;

namespace Axolotl.Application.UseCases.Sales.Queries.GetSalesByDateRange
{
    public sealed class GetSalesByDateRangeQueryHandler
    : IRequestHandler<GetSalesByDateRangeQuery, List<SaleResponse>>
    {
        private readonly IUnitOfWork _uow;

        public GetSalesByDateRangeQueryHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<List<SaleResponse>> Handle(GetSalesByDateRangeQuery request, CancellationToken ct)
        {
            var list = await _uow.Sales.GetByDateRangeAsync(request.From, request.To, ct);

            return list.Select(entity => new SaleResponse
            {
                Id = entity.UUID,
                CustomerId = entity.CustomerId,
                WarehouseId = entity.WarehouseId,
                SaleDate = entity.SaleDate,
                Notes = entity.Notes,
                Items = entity.SaleItems.Select(x => new SaleItemResponse
                {
                    Id = x.UUID,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    DiscountPct = x.DiscountPct
                }).ToList()
            }).ToList();
        }
    }
}
