namespace Axolotl.Domain.Entities;

public class Employees : BaseEntity
{
    public string ID { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime HireDate { get; set; }

    public virtual ICollection<Users> Users { get; set; } = new List<Users>();
}