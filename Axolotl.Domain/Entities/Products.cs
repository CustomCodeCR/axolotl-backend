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

    public virtual Categories Categories { get; set; } = null!;
    public virtual Units Units { get; set; } = null!;
    public virtual TaxRates TaxRates { get; set; } = null!;
    public virtual Suppliers Suppliers { get; set; } = null!;

    public virtual ICollection<ProductPriceHistory> ProductPriceHistories { get; set; } = new List<ProductPriceHistory>();
    public virtual ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
    public virtual ICollection<InventoryMovements> InventoryMovements { get; set; } = new List<InventoryMovements>();
    public ICollection<PurchaseOrderItems> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItems>();
    public ICollection<SupplierInvoiceItems> SupplierInvoiceItems { get; set; } = new List<SupplierInvoiceItems>();
    public virtual ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    public virtual ICollection<SaleItems> SaleItems { get; set; } = new List<SaleItems>();
}