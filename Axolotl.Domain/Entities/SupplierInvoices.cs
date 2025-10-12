namespace Axolotl.Domain.Entities;

public class SupplierInvoices : BaseEntity
{
    public string PurcharseOrderId { get; set; } = null!;
    public string SupplierId { get; set; } = null!;
    public string InvoiceNumber { get; set; } = null!;
    public decimal TotalExTax { get; set; }
    public decimal TotalTax { get; set; }
    public decimal TotalIncTax { get; set; }

    public virtual PurchaseOrders PurchaseOrders { get; set; } = null!;
    public virtual Suppliers Suppliers { get; set; } = null!;
    public ICollection<SupplierInvoiceItems> SupplierInvoiceItems { get; set; } = new List<SupplierInvoiceItems>();
}