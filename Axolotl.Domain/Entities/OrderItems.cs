namespace Axolotl.Domain.Entities;

public class OrderItems : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountPct { get; set; }

    public virtual Orders Orders { get; set; } = null!;
    public virtual Products Products { get; set; } = null!;
}