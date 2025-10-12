using Axolotl.Domain.Enums;

namespace Axolotl.Domain.Entities;

public class Invoices : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid SaleId { get; set; }
    public Guid CustomerId { get; set; }
    public InvoiceStatus Status { get; set; } = InvoiceStatus.DRAFT;
    public string InvoiceNumber { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public Guid BillingAddressId { get; set; }
    public decimal TotalExTax { get; set; }
    public decimal TotalTax { get; set; }
    public decimal TotalIncTax { get; set; }

    public virtual Orders Orders { get; set; } = null!;
    public virtual Sales Sales { get; set; } = null!;
    public virtual CustomerAddresses CustomerAddresses { get; set; } = null!;
    public virtual ICollection<InvoiceItems> InvoiceItems { get; set; } = new List<InvoiceItems>();
    public virtual ICollection<Payments> Payments { get; set; } = new List<Payments>();
    public virtual ICollection<SalesReturns> SalesReturns { get; set; } = new List<SalesReturns>();
}