using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Domain.Entities;
using Axolotl.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Axolotl.Infrastructure.Persistence.Repositories;

public class CartRepository : GenericRepository<Carts>, ICartRepository
{
    private readonly ApplicationDbContext _db;
    public CartRepository(ApplicationDbContext db) : base(db) => _db = db;

    public Task<Carts?> GetByCustomerAsync(Guid customerId, CancellationToken ct) =>
        _db.Set<Carts>()
           .Include(c => c.CartItems)
           .AsNoTracking()
           .FirstOrDefaultAsync(c => c.CustomerId == customerId, ct);
}
