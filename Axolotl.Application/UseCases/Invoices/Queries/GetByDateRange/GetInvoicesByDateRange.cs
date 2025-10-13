using MediatR;
using Axolotl.Application.Dtos.Invoices.Requests;

namespace Axolotl.Application.UseCases.Invoices.Queries.GetInvoicesByDateRange
{
    public sealed record GetInvoicesByDateRangeQuery(DateTime From, DateTime To) : IRequest<List<InvoiceResponse>>;
}
