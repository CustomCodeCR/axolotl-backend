using Axolotl.Application.Commons.Bases;
using Axolotl.Application.Dtos.Sales.Requests;
using Axolotl.Application.Interfaces.Services;
using MediatR;

namespace Axolotl.Application.UseCases.Sales.Queries.GetSaleById
{
    public sealed class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, SaleResponse?>
    {
        private readonly IUnitOfWork _uow;

        public GetSaleByIdQueryHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<SaleResponse?> Handle(GetSaleByIdQuery request, CancellationToken ct)
        {
            var entity = await _uow.Sales.GetWithItemsAsync(request.SaleId, ct);
            if (entity is null) return null;

            var resp = new SaleResponse
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
            };
            return resp;
        }
    }
}
