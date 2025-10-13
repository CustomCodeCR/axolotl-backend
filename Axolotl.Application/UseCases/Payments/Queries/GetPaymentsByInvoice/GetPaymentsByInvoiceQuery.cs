using MediatR;
using Axolotl.Application.Dtos.Payments.Requests;

namespace Axolotl.Application.UseCases.Payments.Queries.GetPaymentsByInvoice
{
    public sealed record GetPaymentsByInvoiceQuery(Guid InvoiceId) : IRequest<List<PaymentResponse>>;
}
