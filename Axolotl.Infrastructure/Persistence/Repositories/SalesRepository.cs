using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Domain.Entities;
using Axolotl.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Axolotl.Infrastructure.Persistence.Repositories
{
    public sealed class SalesRepository : ISalesRepository
    {
        private readonly ApplicationDbContext _db;
        public SalesRepository(ApplicationDbContext db) => _db = db;

        public Task AddAsync(Sales sale, CancellationToken ct = default)
            => _db.Set<Sales>().AddAsync(sale, ct).AsTask();

        public Task<Sales?> GetWithItemsAsync(Guid saleId, CancellationToken ct = default)
            => _db.Set<Sales>()
                  .Include(s => s.SaleItems)
                  .FirstOrDefaultAsync(s => s.UUID == saleId, ct);

        public Task<List<Sales>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct = default)
            => _db.Set<Sales>()
                  .Where(s => s.SaleDate >= from && s.SaleDate <= to)
                  .Include(s => s.SaleItems)
                  .AsNoTracking()
                  .ToListAsync(ct);
    }
}
