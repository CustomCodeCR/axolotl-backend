using MediatR;
using Axolotl.Application.Dtos.Invoices.Requests;
using Axolotl.Application.Interfaces.Services;

namespace Axolotl.Application.UseCases.Invoices.Queries.GetById
{
    public sealed class GetInvoiceByIdQueryHandler
    : IRequestHandler<GetInvoiceByIdQuery, InvoiceResponse?>
    {
        private readonly IUnitOfWork _uow;
        public GetInvoiceByIdQueryHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<InvoiceResponse?> Handle(GetInvoiceByIdQuery q, CancellationToken ct)
        {
            var inv = await _uow.Invoices.GetWithItemsAsync(q.InvoiceUuid, ct);
            if (inv is null) return null;

            return new InvoiceResponse
            {
                UUID = inv.UUID,
                InvoiceNumber = inv.InvoiceNumber,
                SaleId = inv.SaleId,
                CustomerId = inv.CustomerId,
                DueDate = inv.DueDate,
                Status = inv.Status.ToString(),
                TotalExTax = inv.TotalExTax,
                TotalTax = inv.TotalTax,
                TotalIncTax = inv.TotalIncTax,
                Items = inv.InvoiceItems.Select(i => new InvoiceItemResponse
                {
                    UUID = i.UUID,
                    ProductId = i.ProductId,
                    Description = i.Description,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    DiscountPct = i.DiscountPct
                }).ToList()
            };
        }
    }
}
