using Axolotl.Domain.Entities;

namespace Axolotl.Application.Interfaces.Persistence;

public interface IOrderRepository : IGenericRepository<Orders>
{
    Task<Orders?> GetWithItemsAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Orders>> ListWithItemsAsync(CancellationToken ct);
}
