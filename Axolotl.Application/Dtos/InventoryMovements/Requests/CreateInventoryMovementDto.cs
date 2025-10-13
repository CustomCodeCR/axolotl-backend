namespace Axolotl.Application.Dtos.InventoryMovements.Requests;

public class CreateInventoryMovementDto
{
    public Guid WarehouseId { get; set; }
    public Guid ProductId { get; set; }
    public string MovementType { get; set; } = null!; // "IN" | "OUT"
    public int Quantity { get; set; }
    public string ReferenceTable { get; set; } = null!;
    public string ReferenceId { get; set; } = null!;
    public string? Reason { get; set; }
}
