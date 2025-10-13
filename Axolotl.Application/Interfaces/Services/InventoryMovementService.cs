using Axolotl.Application.Dtos.InventoryMovements;
using Axolotl.Application.Dtos.InventoryMovements.Requests;

namespace Axolotl.Application.Interfaces.Services;

public interface IInventoryMovementService
{
    Task<Guid> CreateAsync(CreateInventoryMovementDto dto, CancellationToken ct);
    Task<IReadOnlyList<InventoryMovementResponse>> ListAsync(string? referenceTable, string? referenceId, CancellationToken ct);
}
