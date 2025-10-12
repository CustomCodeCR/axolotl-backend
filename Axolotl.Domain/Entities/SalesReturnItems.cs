namespace Axolotl.Domain.Entities;

public class SalesReturnItems : BaseEntity
{
    public Guid SalesReturnId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public virtual SalesReturns SalesReturns { get; set; } = null!;
    public virtual Products Products { get; set; } = null!;
}