namespace Axolotl.Domain.Entities;

public class ProductStock : BaseEntity
{
    public Guid WarehouseId { get; set; }
    public Guid ProductId { get; set; }
    public int QuantityOnHand { get; set; } = 0;
    public int QuantityReserved { get; set; } = 0;

    public virtual Warehouses Warehouses { get; set; } = null!;
    public virtual Products Products { get; set; } = null!;
}