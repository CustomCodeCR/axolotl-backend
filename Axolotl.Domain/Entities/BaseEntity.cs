using Axolotl.Domain.Enums;

namespace Axolotl.Domain.Entities;

public abstract class BaseEntity
{
    public Guid UUID { get; set; } = Guid.NewGuid();
    public StateType State { get; set; } = StateType.ACTIVE;
    public int? AuditCreateUser { get; set; }
    public DateTime? AuditCreateDate { get; set; }
    public int? AuditUpdateUser { get; set; }
    public DateTime? AuditUpdateDate { get; set; }
    public int? AuditDeleteUser { get; set; }
    public DateTime? AuditDeleteDate { get; set; }
}