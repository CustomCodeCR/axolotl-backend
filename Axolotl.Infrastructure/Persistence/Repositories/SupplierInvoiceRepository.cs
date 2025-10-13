using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Domain.Entities;
using Axolotl.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Axolotl.Infrastructure.Persistence.Repositories;

public class SupplierInvoiceRepository
    : GenericRepository<SupplierInvoices>, ISupplierInvoiceRepository
{
    private readonly ApplicationDbContext _db;

    public SupplierInvoiceRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    /// <summary>
    /// Lista de facturas de proveedor (sin tracking) ordenadas por fecha de creación (auditoría).
    /// </summary>
    public async Task<IReadOnlyList<SupplierInvoices>> ListAsync(CancellationToken ct)
    {
        var list = await _db.Set<SupplierInvoices>()
            .AsNoTracking()
            .OrderByDescending(i => i.AuditCreateDate ?? DateTime.MinValue)
            .ToListAsync(ct);

        return list;
    }

    /// <summary>
    /// Obtiene una factura con sus líneas.
    /// </summary>
    public Task<SupplierInvoices?> GetWithItemsAsync(Guid uuid, CancellationToken ct) =>
        _db.Set<SupplierInvoices>()
           .Include(i => i.SupplierInvoiceItems)
           .AsNoTracking()
           .FirstOrDefaultAsync(i => i.UUID == uuid, ct);
}
