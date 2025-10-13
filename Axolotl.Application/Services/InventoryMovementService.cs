using Axolotl.Application.Dtos.InventoryMovements;
using Axolotl.Application.Dtos.InventoryMovements.Requests;
using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Domain.Entities;
using Axolotl.Domain.Enums;

namespace Axolotl.Application.Services;

public class InventoryMovementService : IInventoryMovementService
{
    private readonly IInventoryMovementRepository _repo;
    private readonly IUnitOfWork _uow;

    public InventoryMovementService(IInventoryMovementRepository repo, IUnitOfWork uow)
    {
        _repo = repo; _uow = uow;
    }

    public async Task<Guid> CreateAsync(CreateInventoryMovementDto dto, CancellationToken ct)
    {
        var entity = new InventoryMovements
        {
            WarehouseId = dto.WarehouseId,
            ProductId = dto.ProductId,
            MovementType = Enum.Parse<InventoryMovementType>(dto.MovementType, true),
            Quantity = dto.Quantity,
            ReferenceTable = dto.ReferenceTable,
            ReferenceId = dto.ReferenceId,
            Reason = dto.Reason
        };

        await _repo.CreateAsync(entity);
        await _uow.SaveChangesAsync(ct);
        return entity.UUID;
    }

    public async Task<IReadOnlyList<InventoryMovementResponse>> ListAsync(string? referenceTable, string? referenceId, CancellationToken ct)
    {
        var list = await _repo.ListByReferenceAsync(referenceTable, referenceId, ct);
        return list.Select(m => new InventoryMovementResponse
        {
            Id = m.UUID,
            WarehouseId = m.WarehouseId,
            ProductId = m.ProductId,
            MovementType = m.MovementType.ToString(),
            Quantity = m.Quantity,
            ReferenceTable = m.ReferenceTable,
            ReferenceId = m.ReferenceId,
            Reason = m.Reason,
            CreatedAt = m.AuditCreateDate ?? DateTime.MinValue   // <- aquí el cambio
        }).ToList();

    }
}
