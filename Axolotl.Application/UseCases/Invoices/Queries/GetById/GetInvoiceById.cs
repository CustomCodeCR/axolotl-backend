using MediatR;
using Axolotl.Application.Dtos.Invoices.Requests;

namespace Axolotl.Application.UseCases.Invoices.Queries.GetById
{
    public sealed record GetInvoiceByIdQuery(Guid InvoiceUuid) : IRequest<InvoiceResponse?>;
}
