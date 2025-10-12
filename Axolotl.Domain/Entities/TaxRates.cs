namespace Axolotl.Domain.Entities;

public class TaxRates : BaseEntity
{
    public string Name { get; set; } = null!;
    public decimal RatePercent { get; set; }

    public ICollection<Products> Products { get; set; } = new List<Products>();
}