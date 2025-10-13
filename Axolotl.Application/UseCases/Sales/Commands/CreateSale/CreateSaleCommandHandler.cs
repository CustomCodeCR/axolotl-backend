using MediatR;
using Axolotl.Application.Interfaces.Services;
using SalesEntity = Axolotl.Domain.Entities.Sales;
using SaleItemEntity = Axolotl.Domain.Entities.SaleItems;

namespace Axolotl.Application.UseCases.Sales.Commands.CreateSale
{
    public sealed class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly IUnitOfWork _uow;

        public CreateSaleCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken ct)
        {
            using var tx = _uow.BeginTransaction();
            try
            {
                var dto = request.Dto;
                var saleDate = dto.SaleDate ?? DateTime.UtcNow;

                var sale = new SalesEntity
                {
                    CustomerId = dto.CustomerId,
                    WarehouseId = dto.WarehouseId,
                    SaleDate = saleDate,
                    Notes = dto.Notes
                };

                foreach (var i in dto.Items)
                {
                    var priceRow = await _uow.ProductPriceHistory.GetCurrentAsync(i.ProductId, saleDate, ct);
                    if (priceRow is null)
                        throw new InvalidOperationException($"No hay precio vigente para producto {i.ProductId} en {saleDate:yyyy-MM-dd HH:mm:ss}.");

                    sale.SaleItems.Add(new SaleItemEntity
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        DiscountPct = i.DiscountPct,   // 0..1
                        UnitPrice = priceRow.Price     // SNAPSHOT
                    });
                }

                await _uow.Sales.AddAsync(sale, ct);
                await _uow.SaveChangesAsync();
                tx.Commit();

                return sale.UUID;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }
    }
}
