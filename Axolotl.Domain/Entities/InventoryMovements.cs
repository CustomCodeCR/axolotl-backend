using Axolotl.Domain.Enums;

namespace Axolotl.Domain.Entities;

public class InventoryMovements : BaseEntity
{
    public Guid WarehouseId { get; set; }
    public Guid ProductId { get; set; }
    public InventoryMovementType MovementType { get; set; }
    public int Quantity { get; set; }
    public string ReferenceTable { get; set; } = null!;
    public string ReferenceId { get; set; } = null!;
    public string? Reason { get; set; }

    public virtual Warehouses Warehouses { get; set; } = null!;
    public virtual Products Products { get; set; } = null!;
}