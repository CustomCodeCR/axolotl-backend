using Axolotl.Domain.Enums;

namespace Axolotl.Domain.Entities;

public class Payments : BaseEntity
{
    public Guid InvoiceId { get; set; }
    public Guid PaymentMethodId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Reference {  get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.COMPLETED;

    public virtual Invoices Invoices { get; set; } = null!;
    public virtual PaymentMethods PaymentMethods { get; set; } = null!;
}