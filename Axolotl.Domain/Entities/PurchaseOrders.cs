using Axolotl.Domain.Enums;

namespace Axolotl.Domain.Entities;

public class PurchaseOrders : BaseEntity
{
    public string SupplierId { get; set; } = null!;
    public string WarehouseId { get; set; } = null!;
    public PurchaseStatus Status { get; set; } = PurchaseStatus.DRAFT;
    public DateTime OrderDate { get; set; }
    public DateTime ExpectedDate { get; set; }
    public string? Notes { get; set; }

    public virtual Suppliers Suppliers { get; set; } = null!;
    public virtual Warehouses Warehouses { get; set; } = null!;
    public ICollection<PurchaseOrderItems> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItems>();
    public ICollection<SupplierInvoices> SupplierInvoices { get; set; } = new List<SupplierInvoices>();
}