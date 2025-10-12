namespace Axolotl.Domain.Entities;

public class Categories : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<Products> Products { get; set; } = new List<Products>();
}