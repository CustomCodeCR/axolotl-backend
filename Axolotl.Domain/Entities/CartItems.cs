namespace Axolotl.Domain.Entities;

public class CartItems : BaseEntity
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public virtual Carts Carts { get; set; } = null!;
    public virtual Products Products { get; set; } = null!;
}