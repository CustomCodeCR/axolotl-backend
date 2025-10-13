using MediatR;
using Axolotl.Application.Dtos.Payments.Requests;
using Axolotl.Application.Interfaces.Services;

namespace Axolotl.Application.UseCases.Payments.Queries.GetPaymentsByInvoice
{
    public sealed class GetPaymentsByInvoiceQueryHandler
    : IRequestHandler<GetPaymentsByInvoiceQuery, List<PaymentResponse>>
    {
        private readonly IUnitOfWork _uow;
        public GetPaymentsByInvoiceQueryHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<List<PaymentResponse>> Handle(GetPaymentsByInvoiceQuery request, CancellationToken ct)
        {
            var list = await _uow.Payments.GetByInvoiceAsync(request.InvoiceId, ct);
            return list.Select(p => new PaymentResponse
            {
                UUID = p.UUID,
                InvoiceId = p.InvoiceId,
                PaymentMethodId = p.PaymentMethodId,
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                Reference = p.Reference,
                Status = p.Status
            }).ToList();
        }
    }
}
