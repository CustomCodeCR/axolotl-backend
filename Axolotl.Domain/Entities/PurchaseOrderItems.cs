namespace Axolotl.Domain.Entities;

public class PurchaseOrderItems : BaseEntity
{
    public string PurchaseOrderId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }

    public virtual PurchaseOrders PurchaseOrders { get; set; } = null!;
    public virtual Products Products { get; set; } = null!;
}