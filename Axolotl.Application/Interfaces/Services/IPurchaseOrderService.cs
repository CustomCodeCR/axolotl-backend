using Axolotl.Application.Dtos.PurchaseOrders;
using Axolotl.Application.Dtos.PurchaseOrders.Requests;


namespace Axolotl.Application.Interfaces.Services;

public interface IPurchaseOrderService
{
    Task<Guid> CreateAsync(CreatePurchaseOrderDto dto, CancellationToken ct);
    Task<IReadOnlyList<PurchaseOrderListResponse>> ListAsync(CancellationToken ct);
    Task<PurchaseOrderDetailResponse?> GetAsync(Guid id, CancellationToken ct);
}
