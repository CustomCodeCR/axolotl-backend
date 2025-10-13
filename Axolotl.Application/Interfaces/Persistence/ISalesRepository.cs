using Axolotl.Domain.Entities;

namespace Axolotl.Application.Interfaces.Persistence
{
    public interface ISalesRepository
    {
        Task AddAsync(Sales sale, CancellationToken ct = default);
        Task<Sales?> GetWithItemsAsync(Guid saleId, CancellationToken ct = default);
        Task<List<Sales>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct = default);
    }
}
