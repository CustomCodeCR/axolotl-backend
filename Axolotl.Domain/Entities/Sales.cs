using Axolotl.Domain.Enums;

namespace Axolotl.Domain.Entities;

public class Sales : BaseEntity
{

    public Guid CustomerId { get; set; }
    public Guid WarehouseId { get; set; }
    public SaleStatus Status { get; set; } = SaleStatus.OPEN;
    public DateTime SaleDate { get; set; }
    public string? Notes { get; set; }

    public virtual Customers Customers { get; set; } = null!;
    public virtual Warehouses Warehouses { get; set; } = null!;
    public virtual ICollection<SaleItems> SaleItems { get; set; } = new List<SaleItems>();
    public virtual ICollection<Invoices> Invoices { get; set; } = new List<Invoices>();
}