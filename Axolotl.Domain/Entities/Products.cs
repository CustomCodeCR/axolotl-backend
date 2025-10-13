namespace Axolotl.Domain.Entities;

public class Products : BaseEntity
{
    public string SKU { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid CategoryId { get; set; }
    public Guid UnitId { get; set; }
    public Guid TaxRateId { get; set; }
    public Guid SupplierId { get; set; }

    public virtual Category Category { get; set; } = null!;
    public virtual Units Units { get; set; } = null!;
    public virtual TaxRates TaxRates { get; set; } = null!;
    public virtual Suppliers Suppliers { get; set; } = null!;

    public virtual ICollection<ProductPriceHistory> ProductPriceHistories { get; set; } = new List<ProductPriceHistory>();
    public virtual ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
    public virtual ICollection<InventoryMovements> InventoryMovements { get; set; } = new List<InventoryMovements>();
    public virtual ICollection<PurchaseOrderItems> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItems>();
    public virtual ICollection<SupplierInvoiceItems> SupplierInvoiceItems { get; set; } = new List<SupplierInvoiceItems>();
    public virtual ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    public virtual ICollection<SaleItems> SaleItems { get; set; } = new List<SaleItems>();
    public virtual ICollection<InvoiceItems> InvoiceItems { get; set; } = new List<InvoiceItems>();
    public virtual ICollection<SalesReturnItems> ReturnItems { get; set; } = new List<SalesReturnItems>();
    public virtual ICollection<CartItems> CartItems { get; set; } = new List<CartItems>();
}