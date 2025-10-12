namespace Axolotl.Domain.Entities;

public class PaymentMethods : BaseEntity
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Payments> Payments { get; set; } = new List<Payments>();
}