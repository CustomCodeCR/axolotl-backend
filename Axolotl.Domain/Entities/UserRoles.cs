namespace Axolotl.Domain.Entities;

public class UserRoles : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    public virtual Users Users { get; set; } = null!;
    public virtual Roles Roles { get; set; } = null!;
}