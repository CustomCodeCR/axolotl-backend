namespace Axolotl.Domain.Entities;

public class Suppliers : BaseEntity
{
    public string ID { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public virtual ICollection<Products> Products { get; set; } = new List<Products>();
}