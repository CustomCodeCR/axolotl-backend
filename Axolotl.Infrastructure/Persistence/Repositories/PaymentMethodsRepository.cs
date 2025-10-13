using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Domain.Entities;
using Axolotl.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Axolotl.Infrastructure.Persistence.Repositories
{
    public sealed class PaymentMethodsRepository : IPaymentMethodsRepository
    {
        private readonly ApplicationDbContext _db;
        public PaymentMethodsRepository(ApplicationDbContext db) => _db = db;

        public Task<PaymentMethods?> GetByIdAsync(Guid uuid, CancellationToken ct = default)
            => _db.Set<PaymentMethods>().FirstOrDefaultAsync(x => x.UUID == uuid, ct);

        public Task<PaymentMethods?> GetByCodeAsync(string code, CancellationToken ct = default)
            => _db.Set<PaymentMethods>().FirstOrDefaultAsync(x => x.Code == code, ct);

        public Task<bool> ExistsAsync(Guid uuid, CancellationToken ct = default)
            => _db.Set<PaymentMethods>().AnyAsync(x => x.UUID == uuid, ct);

        public Task<List<PaymentMethods>> GetAllAsync(CancellationToken ct = default)
            => _db.Set<PaymentMethods>().AsNoTracking().OrderBy(x => x.Name).ToListAsync(ct);
    }
}
