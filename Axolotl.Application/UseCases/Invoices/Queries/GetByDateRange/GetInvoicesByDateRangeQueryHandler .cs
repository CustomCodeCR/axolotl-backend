using MediatR;
using Axolotl.Application.Dtos.Invoices.Requests;
using Axolotl.Application.Interfaces.Services;

namespace Axolotl.Application.UseCases.Invoices.Queries.GetInvoicesByDateRange
{
    public sealed class GetInvoicesByDateRangeQueryHandler
    : IRequestHandler<GetInvoicesByDateRangeQuery, List<InvoiceResponse>>
    {
        private readonly IUnitOfWork _uow;
        public GetInvoicesByDateRangeQueryHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<List<InvoiceResponse>> Handle(GetInvoicesByDateRangeQuery q, CancellationToken ct)
        {
            var list = await _uow.Invoices.GetByDateRangeAsync(q.From, q.To, ct);
            return list.Select(inv => new InvoiceResponse
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
            }).ToList();
        }
    }
}
