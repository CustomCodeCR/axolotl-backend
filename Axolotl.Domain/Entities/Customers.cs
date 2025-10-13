namespace Axolotl.Domain.Entities;

public class Customers : BaseEntity
{
    public string ID { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public virtual ICollection<CustomerAddresses> CustomerAddresses { get; set; } = new List<CustomerAddresses>();
    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
    public virtual ICollection<Sales> Sales { get; set; } = new List<Sales>();
    public virtual ICollection<Carts> Carts { get; set; } = new List<Carts>();
}