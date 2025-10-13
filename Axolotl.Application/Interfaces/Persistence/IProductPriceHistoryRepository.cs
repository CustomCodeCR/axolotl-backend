using Axolotl.Domain.Entities;

namespace Axolotl.Application.Interfaces.Persistence
{
    public interface IProductPriceHistoryRepository
    {
        Task<ProductPriceHistory?> GetCurrentAsync(Guid productId, DateTime at, CancellationToken ct = default);
        Task<ProductPriceHistory?> GetLastAsync(Guid productId, CancellationToken ct = default);
        Task<List<ProductPriceHistory>> GetByProductAsync(Guid productId, CancellationToken ct = default);
        Task AddAsync(ProductPriceHistory row, CancellationToken ct = default);
        Task UpdateAsync(ProductPriceHistory row, CancellationToken ct = default);
    }
}
