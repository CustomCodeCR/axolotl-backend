namespace Axolotl.Application.Dtos.PurchaseOrders;

public record PurchaseOrderListResponse(
    Guid Id, Guid SupplierId, Guid WarehouseId, DateTime ExpectedDate, string? Notes, int ItemsCount);
