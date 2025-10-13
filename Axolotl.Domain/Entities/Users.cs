namespace Axolotl.Domain.Entities;

public class Users : BaseEntity
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Guid EmployeeId { get; set; }
    public DateTime LastLoginAt {  get; set; }

    public virtual Employees Employees { get; set; } = null!;
    public virtual ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
}