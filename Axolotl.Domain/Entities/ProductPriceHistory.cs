namespace Axolotl.Domain.Entities;

public class ProductPriceHistory : BaseEntity
{
    public string ProductId { get; set; } = null!;
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public string? Note { get; set; }

    public virtual Products Products { get; set; } = null!;
}