using System.Linq;
using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Domain.Entities;
using Axolotl.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Axolotl.Infrastructure.Persistence.Repositories;

public class InventoryMovementRepository : GenericRepository<InventoryMovements>, IInventoryMovementRepository
{
    private readonly ApplicationDbContext _db;
    public InventoryMovementRepository(ApplicationDbContext db) : base(db) => _db = db;

    public async Task<IReadOnlyList<InventoryMovements>> ListByReferenceAsync(
        string? referenceTable, string? referenceId, CancellationToken ct)
    {
        var q = _db.Set<InventoryMovements>().AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(referenceTable))
            q = q.Where(x => x.ReferenceTable == referenceTable);

        if (!string.IsNullOrWhiteSpace(referenceId))
            q = q.Where(x => x.ReferenceId == referenceId);

        return await q
            .OrderByDescending(x => x.AuditCreateDate ?? DateTime.MinValue)
            .ToListAsync(ct);
    }
}
