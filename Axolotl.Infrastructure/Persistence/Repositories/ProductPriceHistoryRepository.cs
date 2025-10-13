using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Domain.Entities;
using Axolotl.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Axolotl.Infrastructure.Persistence.Repositories
{
    public sealed class ProductPriceHistoryRepository : IProductPriceHistoryRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductPriceHistoryRepository(ApplicationDbContext db) => _db = db;

        public Task<ProductPriceHistory?> GetCurrentAsync(Guid productId, DateTime at, CancellationToken ct = default)
            => _db.Set<ProductPriceHistory>()
                  .Where(p => p.ProductId == productId && p.ValidFrom <= at && at <= p.ValidTo)
                  .OrderByDescending(p => p.ValidFrom)
                  .AsNoTracking()
                  .FirstOrDefaultAsync(ct);

        public Task<ProductPriceHistory?> GetLastAsync(Guid productId, CancellationToken ct = default)
            => _db.Set<ProductPriceHistory>()
                  .Where(p => p.ProductId == productId)
                  .OrderByDescending(p => p.ValidFrom)
                  .AsNoTracking()
                  .FirstOrDefaultAsync(ct);

        public Task<List<ProductPriceHistory>> GetByProductAsync(Guid productId, CancellationToken ct = default)
            => _db.Set<ProductPriceHistory>()
                  .Where(p => p.ProductId == productId)
                  .OrderByDescending(p => p.ValidFrom)
                  .AsNoTracking()
                  .ToListAsync(ct);

        public Task AddAsync(ProductPriceHistory row, CancellationToken ct = default)
            => _db.Set<ProductPriceHistory>().AddAsync(row, ct).AsTask();

        public Task UpdateAsync(ProductPriceHistory row, CancellationToken ct = default)
        {
            _db.Set<ProductPriceHistory>().Update(row);
            return Task.CompletedTask;
        }
    }
}
