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
}