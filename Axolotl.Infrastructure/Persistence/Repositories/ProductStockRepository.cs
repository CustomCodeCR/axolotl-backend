using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Domain.Entities;
using Axolotl.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Axolotl.Infrastructure.Persistence.Repositories
{
    public sealed class ProductStockRepository : IProductStockRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductStockRepository(ApplicationDbContext db) => _db = db;

        public Task<List<ProductStock>> GetByWarehouseAsync(Guid warehouseId, CancellationToken ct = default)
            => _db.Set<ProductStock>()
                  .Where(s => s.WarehouseId == warehouseId)
                  .AsNoTracking()
                  .ToListAsync(ct);
    }
}
