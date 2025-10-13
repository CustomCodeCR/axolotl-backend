using Axolotl.Domain.Entities;

namespace Axolotl.Application.Interfaces.Persistence;

public interface ICartRepository : IGenericRepository<Carts>
{
    Task<Carts?> GetByCustomerAsync(Guid customerId, CancellationToken ct);
}
