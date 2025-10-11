namespace Axolotl.Domain.Entities;

public class Products : BaseEntity
{
    public string SKU { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string CategoryId { get; set; } = null!;
    public string UnitId { get; set; } = null!;
    public string TaxRateId { get; set; } = null!;
    public string SupplierId { get; set; } = null!;

    public virtual Categories Categories { get; set; } = null!;
    public virtual Units Units { get; set; } = null!;
    public virtual TaxRates TaxRates { get; set; } = null!;
    public virtual Suppliers Suppliers { get; set; } = null!;

    public virtual ICollection<ProductPriceHistory> ProductPriceHistories { get; set; } = new List<ProductPriceHistory>();
    public virtual ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
    public virtual ICollection<InventoryMovements> InventoryMovements { get; set; } = new List<InventoryMovements>();
}