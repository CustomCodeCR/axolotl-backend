namespace Axolotl.Domain.Entities;

public class Units : BaseEntity
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;

    public ICollection<Products> Products { get; set; } = new List<Products>();
}