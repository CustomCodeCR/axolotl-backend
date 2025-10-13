using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Domain.Entities;
using Axolotl.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Axolotl.Infrastructure.Persistence.Repositories
{
    public sealed class InvoicesRepository : IInvoicesRepository
    {
        private readonly ApplicationDbContext _db;
        public InvoicesRepository(ApplicationDbContext db) => _db = db;

        public Task AddAsync(Invoices invoice, CancellationToken ct = default)
            => _db.Set<Invoices>().AddAsync(invoice, ct).AsTask();

        public Task<bool> ExistsNumberAsync(string invoiceNumber, CancellationToken ct = default)
            => _db.Set<Invoices>()
                  .AsNoTracking()
                  .AnyAsync(x => x.InvoiceNumber == invoiceNumber, ct);

        public Task<Invoices?> GetWithItemsAsync(Guid invoiceUuid, CancellationToken ct = default)
            => _db.Set<Invoices>()
                  .Include(x => x.InvoiceItems)
                  .FirstOrDefaultAsync(x => x.UUID == invoiceUuid, ct);

        public Task<List<Invoices>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct = default)
            => _db.Set<Invoices>()
                  .Where(x => x.DueDate >= from && x.DueDate <= to)
                  .Include(x => x.InvoiceItems)
                  .AsNoTracking()
                  .ToListAsync(ct);
    }
}
