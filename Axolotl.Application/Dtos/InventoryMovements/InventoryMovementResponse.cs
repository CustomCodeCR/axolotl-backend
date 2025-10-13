namespace Axolotl.Application.Dtos.InventoryMovements;

public class InventoryMovementResponse
{
    public Guid Id { get; set; }          
    public Guid WarehouseId { get; set; }
    public Guid ProductId { get; set; }
    public string MovementType { get; set; } = null!;
    public int Quantity { get; set; }
    public string ReferenceTable { get; set; } = null!;
    public string ReferenceId { get; set; } = null!;
    public string? Reason { get; set; }
    public DateTime CreatedAt { get; set; }
}
