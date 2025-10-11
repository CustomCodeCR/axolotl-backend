namespace Axolotl.Domain.Entities;

public class ProductStock : BaseEntity
{
    public string WarehouseId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public int QuantityOnHand { get; set; } = 0;
    public int QuantityOnReserved { get; set; } = 0;

    public virtual Warehouses Warehouses { get; set; } = null!;
    public virtual Products Products { get; set; } = null!;
}