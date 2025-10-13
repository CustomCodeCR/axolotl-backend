namespace Axolotl.Application.Dtos.PurchaseOrders.Requests;

public record CreatePurchaseOrderItemDto(Guid ProductId, int Quantity, decimal UnitCost);
public record CreatePurchaseOrderDto(Guid SupplierId, Guid WarehouseId, DateTime ExpectedDate, string? Notes,
                                     List<CreatePurchaseOrderItemDto> Items);
