using Axolotl.Domain.Enums;

namespace Axolotl.Domain.Entities;

public class Orders : BaseEntity
{
    public Guid CustomerId { get; set; }
    public Guid WarehouseId { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.DRAFT;
    public DateTime OrderDate { get; set; }
    public DateTime RequiredDate { get; set; }
    public string? Notes { get; set; }

    public virtual Customers Customers { get; set; } = null!;
    public virtual Warehouses Warehouses { get; set; } = null!;
    public virtual ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    public virtual ICollection<Invoices> Invoices { get; set; } = new List<Invoices>();
}