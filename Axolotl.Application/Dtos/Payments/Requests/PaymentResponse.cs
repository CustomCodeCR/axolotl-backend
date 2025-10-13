using Axolotl.Domain.Enums;

namespace Axolotl.Application.Dtos.Payments.Requests
{
    public sealed class PaymentResponse
    {
        public Guid UUID { get; init; }
        public Guid InvoiceId { get; init; }
        public Guid PaymentMethodId { get; init; }
        public decimal Amount { get; init; }
        public DateTime PaymentDate { get; init; }
        public string? Reference { get; init; }
        public PaymentStatus Status { get; init; }
    }
}
