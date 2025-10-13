using MediatR;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Application.UseCases.Payments.Commands.CapturePayment;

namespace Axolotl.Application.UseCases.Payments.Commands.UpdatePaymentStatus
{
    public sealed class UpdatePaymentStatusCommandHandler
        : IRequestHandler<UpdatePaymentStatusCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        public UpdatePaymentStatusCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Unit> Handle(UpdatePaymentStatusCommand request, CancellationToken ct)
        {
            using var tx = _uow.BeginTransaction();
            try
            {
                var dto = request.Dto;
                var payment = await _uow.Payments.GetByIdAsync(dto.PaymentId, ct)
                              ?? throw new KeyNotFoundException($"Payment {dto.PaymentId} no existe.");

                payment.Status = dto.Status;
                await _uow.Payments.UpdateAsync(payment, ct);
                await _uow.SaveChangesAsync();

                var helper = new CapturePaymentCommandHandler(_uow);
                await helper.__UpdateInvoiceStatusAsync(payment.InvoiceId, ct);

                tx.Commit();
                return Unit.Value;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }
    }
}
