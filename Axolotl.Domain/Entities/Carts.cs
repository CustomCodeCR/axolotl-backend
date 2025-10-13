using Axolotl.Domain.Enums;

namespace Axolotl.Domain.Entities;

public class Carts : BaseEntity
{
    public Guid CustomerId { get; set; }
    public CartStatus Status { get; set; } = CartStatus.OPEN;

    public virtual Customers Customers { get; set; } = null!;
    public virtual ICollection<CartItems> CartItems { get; set; } = new List<CartItems>();
}