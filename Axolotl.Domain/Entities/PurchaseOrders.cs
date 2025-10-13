using Axolotl.Domain.Enums;

namespace Axolotl.Domain.Entities;

public class PurchaseOrders : BaseEntity
{
    public Guid SupplierId { get; set; }
    public Guid WarehouseId { get; set; }
    public PurchaseStatus Status { get; set; } = PurchaseStatus.DRAFT;
    public DateTime OrderDate { get; set; }
    public DateTime ExpectedDate { get; set; }
    public string? Notes { get; set; }

    public virtual Suppliers Suppliers { get; set; } = null!;
    public virtual Warehouses Warehouses { get; set; } = null!;
    public virtual ICollection<PurchaseOrderItems> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItems>();
    public virtual ICollection<SupplierInvoices> SupplierInvoices { get; set; } = new List<SupplierInvoices>();
}