namespace Axolotl.Application.Dtos.PurchaseOrders;

public record PurchaseOrderDetailItem(Guid ProductId, int Quantity, decimal UnitCost);
public record PurchaseOrderDetailResponse(
    Guid Id, Guid SupplierId, Guid WarehouseId, DateTime ExpectedDate, string? Notes,
    IReadOnlyList<PurchaseOrderDetailItem> Items);
