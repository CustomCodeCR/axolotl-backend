namespace Axolotl.Domain.Entities;

public class ProductPriceHistory : BaseEntity
{
    public Guid ProductId { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public string? Note { get; set; }

    public virtual Products Products { get; set; } = null!;
}