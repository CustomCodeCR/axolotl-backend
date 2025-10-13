using MediatR;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Application.Dtos.Payments.Requests;
using Axolotl.Domain.Enums;
using PaymentEntity = Axolotl.Domain.Entities.Payments;

namespace Axolotl.Application.UseCases.Payments.Commands.CapturePayment
{
    public sealed class CapturePaymentCommandHandler : IRequestHandler<CapturePaymentCommand, Guid>
    {
        private readonly IUnitOfWork _uow;
        public CapturePaymentCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Guid> Handle(CapturePaymentCommand request, CancellationToken ct)
        {
            using var tx = _uow.BeginTransaction();
            try
            {
                var dto = request.Dto;
                var when = dto.PaymentDate ?? DateTime.UtcNow;

                var invoice = await _uow.Invoices.GetWithItemsAsync(dto.InvoiceId, ct)
                              ?? throw new InvalidOperationException($"Invoice {dto.InvoiceId} no existe.");

                if (dto.Amount <= 0) throw new InvalidOperationException("El monto debe ser mayor a 0.");

                var methodExists = await _uow.PaymentMethods.ExistsAsync(dto.PaymentMethodId, ct);
                if (!methodExists) throw new InvalidOperationException($"PaymentMethod {dto.PaymentMethodId} no existe.");

                var payment = new PaymentEntity
                {
                    InvoiceId = invoice.UUID,
                    PaymentMethodId = dto.PaymentMethodId,
                    Amount = dto.Amount,
                    PaymentDate = when,
                    Reference = dto.Reference,
                    Status = PaymentStatus.COMPLETED
                };

                await _uow.Payments.AddAsync(payment, ct);
                await _uow.SaveChangesAsync();

                await UpdateInvoiceStatusAsync(invoice.UUID, ct);

                tx.Commit();
                return payment.UUID;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        private async Task UpdateInvoiceStatusAsync(Guid invoiceUuid, CancellationToken ct)
        {
            var totalPaid = await _uow.Payments.GetTotalCompletedByInvoiceAsync(invoiceUuid, ct);
            var inv = await _uow.Invoices.GetWithItemsAsync(invoiceUuid, ct)
                      ?? throw new InvalidOperationException("Factura no encontrada para actualizar estado.");

            var due = inv.TotalIncTax;

            if (totalPaid <= 0m)
                inv.Status = InvoiceStatus.DRAFT;
            else
                inv.Status = InvoiceStatus.PAID;

            await _uow.SaveChangesAsync();
        }

        internal Task __UpdateInvoiceStatusAsync(Guid invoiceUuid, CancellationToken ct)
            => UpdateInvoiceStatusAsync(invoiceUuid, ct);
    }
}
