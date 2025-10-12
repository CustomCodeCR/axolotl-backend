namespace Axolotl.Domain.Entities;

public class PurchaseOrderItems : BaseEntity
{
    public Guid PurchaseOrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }

    public virtual PurchaseOrders PurchaseOrders { get; set; } = null!;
    public virtual Products Products { get; set; } = null!;
}