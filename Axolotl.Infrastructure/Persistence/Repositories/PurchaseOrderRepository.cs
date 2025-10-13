using System.Linq;
using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Domain.Entities;
using Axolotl.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Axolotl.Infrastructure.Persistence.Repositories;

public class PurchaseOrderRepository : GenericRepository<PurchaseOrders>, IPurchaseOrderRepository
{
    private readonly ApplicationDbContext _db;
    public PurchaseOrderRepository(ApplicationDbContext db) : base(db) => _db = db;

    public Task<PurchaseOrders?> GetWithItemsAsync(Guid uuid, CancellationToken ct) =>
        _db.Set<PurchaseOrders>()
           .Include(o => o.PurchaseOrderItems)
           .AsNoTracking()
           .FirstOrDefaultAsync(o => o.UUID == uuid, ct);

    public async Task<IReadOnlyList<PurchaseOrders>> ListWithItemsAsync(CancellationToken ct)
    {
        var list = await _db.Set<PurchaseOrders>()
            .Include(o => o.PurchaseOrderItems)
            .AsNoTracking()
            .OrderByDescending(o => o.AuditCreateDate ?? o.ExpectedDate)
            .ToListAsync(ct);

        return list;
    }
}
