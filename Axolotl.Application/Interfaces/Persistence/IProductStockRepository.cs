using Axolotl.Domain.Entities;

namespace Axolotl.Application.Interfaces.Persistence
{
    public interface IProductStockRepository
    {
        Task<List<ProductStock>> GetByWarehouseAsync(Guid warehouseId, CancellationToken ct = default); // read-only
    }
}
