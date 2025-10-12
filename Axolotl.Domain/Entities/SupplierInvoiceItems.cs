namespace Axolotl.Domain.Entities;

public class SupplierInvoiceItems : BaseEntity
{
    public Guid SupplierInvoiceId { get; set; }
    public Guid ProductId { get; set; }
    public string? Description { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal DiscountPct { get; set; }

    public virtual SupplierInvoices SupplierInvoices { get; set; } = null!;
    public virtual Products Products { get; set; } = null!;
}