using Axolotl.Domain.Entities;

namespace Axolotl.Application.Interfaces.Persistence;

public interface IPurchaseOrderRepository : IGenericRepository<PurchaseOrders>
{
    Task<PurchaseOrders?> GetWithItemsAsync(Guid uuid, CancellationToken ct);
    Task<IReadOnlyList<PurchaseOrders>> ListWithItemsAsync(CancellationToken ct);
}
