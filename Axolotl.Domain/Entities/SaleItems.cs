namespace Axolotl.Domain.Entities;

public class SaleItems : BaseEntity
{
    public Guid SaleId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountPct { get; set; }

    public virtual Sales Sales { get; set; } = null!;
    public virtual Products Products { get; set; } = null!;
}