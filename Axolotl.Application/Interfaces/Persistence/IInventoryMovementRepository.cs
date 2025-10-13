using Axolotl.Domain.Entities;

namespace Axolotl.Application.Interfaces.Persistence;

public interface IInventoryMovementRepository : IGenericRepository<InventoryMovements>
{
    Task<IReadOnlyList<InventoryMovements>> ListByReferenceAsync(string? referenceTable, string? referenceId, CancellationToken ct);
}
