using Axolotl.Domain.Enums;

namespace Axolotl.Application.Dtos.Payments.Requests
{
    public sealed class UpdatePaymentStatusDto
    {
        public Guid PaymentId { get; init; }
        public PaymentStatus Status { get; init; }    // COMPLETED / FAILED / REFUNDED / PENDING...
    }
}
