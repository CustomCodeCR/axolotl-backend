using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Domain.Entities;
using Axolotl.Domain.Enums;
using Axolotl.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Axolotl.Infrastructure.Persistence.Repositories
{
    public sealed class PaymentsRepository : IPaymentsRepository
    {
        private readonly ApplicationDbContext _db;
        public PaymentsRepository(ApplicationDbContext db) => _db = db;

        public Task AddAsync(Payments entity, CancellationToken ct = default)
            => _db.Set<Payments>().AddAsync(entity, ct).AsTask();

        public Task UpdateAsync(Payments entity, CancellationToken ct = default)
        { _db.Set<Payments>().Update(entity); return Task.CompletedTask; }

        public Task<Payments?> GetByIdAsync(Guid paymentUuid, CancellationToken ct = default)
            => _db.Set<Payments>().FirstOrDefaultAsync(x => x.UUID == paymentUuid, ct);

        public Task<List<Payments>> GetByInvoiceAsync(Guid invoiceUuid, CancellationToken ct = default)
            => _db.Set<Payments>()
                  .Where(x => x.InvoiceId == invoiceUuid)
                  .AsNoTracking()
                  .ToListAsync(ct);

        public Task<decimal> GetTotalCompletedByInvoiceAsync(Guid invoiceUuid, CancellationToken ct = default)
            => _db.Set<Payments>()
                  .Where(x => x.InvoiceId == invoiceUuid && x.Status == PaymentStatus.COMPLETED)
                  .Select(x => x.Amount)
                  .DefaultIfEmpty(0m)
                  .SumAsync(ct);
    }
}
