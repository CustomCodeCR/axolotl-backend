using System.Linq;
using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Domain.Entities;
using Axolotl.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Axolotl.Infrastructure.Persistence.Repositories;

public class OrderRepository : GenericRepository<Orders>, IOrderRepository
{
    private readonly ApplicationDbContext _db;
    public OrderRepository(ApplicationDbContext db) : base(db) => _db = db;

    public Task<Orders?> GetWithItemsAsync(Guid uuid, CancellationToken ct) =>
        _db.Set<Orders>().Include(o => o.OrderItems).AsNoTracking()
           .FirstOrDefaultAsync(o => o.UUID == uuid, ct);

    public async Task<IReadOnlyList<Orders>> ListWithItemsAsync(CancellationToken ct)
    {
        var list = await _db.Set<Orders>()
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .OrderByDescending(o => o.AuditCreateDate ?? o.OrderDate)
            .ToListAsync(ct);
        return list;
    }
}
