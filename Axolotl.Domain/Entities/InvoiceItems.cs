namespace Axolotl.Domain.Entities;

public class InvoiceItems : BaseEntity
{
    public Guid InvoiceId { get; set; }
    public Guid ProductId { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountPct { get; set; }

    public virtual Invoices Invoices { get; set; } = null!;
    public virtual Products Products { get; set; } = null!;
}