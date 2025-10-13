using MediatR;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Domain.Enums;
using InvoiceEntity = Axolotl.Domain.Entities.Invoices;
using InvoiceItemEntity = Axolotl.Domain.Entities.InvoiceItems;

namespace Axolotl.Application.UseCases.Invoices.Commands.CreateFromSale
{
    public sealed class CreateInvoiceFromSaleCommandHandler
    : IRequestHandler<CreateInvoiceFromSaleCommand, Guid>
    {
        private readonly IUnitOfWork _uow;

        public CreateInvoiceFromSaleCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Guid> Handle(CreateInvoiceFromSaleCommand request, CancellationToken ct)
        {
            using var tx = _uow.BeginTransaction();
            try
            {
                var dto = request.Dto;

                var sale = await _uow.Sales.GetWithItemsAsync(dto.SaleId, ct)
                           ?? throw new InvalidOperationException($"Sale {dto.SaleId} no existe.");

                var saleDate = sale.SaleDate;
                var dueDate = dto.DueDate ?? saleDate.AddDays(30);

                string number = await GenerateUniqueNumberAsync(ct);

                var invoice = new InvoiceEntity
                {
                    SaleId = sale.UUID,
                    CustomerId = dto.CustomerId,
                    BillingAddressId = dto.BillingAddressId,
                    DueDate = dueDate,
                    InvoiceNumber = number,
                    Status = InvoiceStatus.DRAFT
                };

                decimal totalEx = 0m;

                foreach (var s in sale.SaleItems)
                {
                    var line = new InvoiceItemEntity
                    {
                        ProductId = s.ProductId,
                        Description = null,
                        Quantity = s.Quantity,
                        UnitPrice = s.UnitPrice,
                        DiscountPct = s.DiscountPct
                    };
                    invoice.InvoiceItems.Add(line);

                    totalEx += s.Quantity * s.UnitPrice * (1 - s.DiscountPct);
                }

                invoice.TotalExTax = Math.Round(totalEx, 2);
                invoice.TotalTax = 0m;
                invoice.TotalIncTax = invoice.TotalExTax + 0m;

                await _uow.Invoices.AddAsync(invoice, ct);
                await _uow.SaveChangesAsync();
                tx.Commit();

                return invoice.UUID;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        private async Task<string> GenerateUniqueNumberAsync(CancellationToken ct)
        {
            // Formato: INV-YYYYMMDD-XXXXXX
            const int maxRetries = 5;
            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                var stamp = DateTime.UtcNow.ToString("yyyyMMdd");
                var rand = Random.Shared.Next(0, 999999).ToString("D6");
                var candidate = $"INV-{stamp}-{rand}";

                var exists = await _uow.Invoices.ExistsNumberAsync(candidate, ct);
                if (!exists) return candidate;
            }
            // Último recurso: Guid corto
            return $"INV-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid():N}".Substring(0, 22);
        }
    }
}
