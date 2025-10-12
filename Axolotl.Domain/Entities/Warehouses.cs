namespace Axolotl.Domain.Entities;

public class Warehouses : BaseEntity
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Province { get; set; } = null!;
    public string Canton { get; set; } = null!;
    public string Distric { get; set; } = null!;
    public string Country { get; set; } = null!;

    public virtual ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
    public virtual ICollection<InventoryMovements> InventoryMovements { get; set; } = new List<InventoryMovements>();
    public virtual ICollection<PurchaseOrders> PurchaseOrders { get; set; } = new List<PurchaseOrders>();
    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
    public virtual ICollection<Sales> Sales { get; set; } = new List<Sales>();
}