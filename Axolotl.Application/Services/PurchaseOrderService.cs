using Axolotl.Application.Dtos.PurchaseOrders;
using Axolotl.Application.Dtos.PurchaseOrders.Requests;
using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Domain.Entities;
using Axolotl.Domain.Enums;

namespace Axolotl.Application.Services;

public class PurchaseOrderService : IPurchaseOrderService
{
    private readonly IPurchaseOrderRepository _repo;
    private readonly IUnitOfWork _uow;

    public PurchaseOrderService(IPurchaseOrderRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Guid> CreateAsync(CreatePurchaseOrderDto dto, CancellationToken ct)
    {
        var po = new PurchaseOrders
        {
            SupplierId = dto.SupplierId,
            WarehouseId = dto.WarehouseId,
            Status = PurchaseStatus.DRAFT,
            OrderDate = DateTime.UtcNow,
            ExpectedDate = dto.ExpectedDate,
            Notes = dto.Notes
        };

        foreach (var it in dto.Items)
        {
            po.PurchaseOrderItems.Add(new PurchaseOrderItems
            {
                ProductId = it.ProductId,
                Quantity = it.Quantity,
                UnitCost = it.UnitCost
            });
        }

        await _repo.CreateAsync(po);
        await _uow.SaveChangesAsync(ct);
        return po.UUID;
    }

    public async Task<IReadOnlyList<PurchaseOrderListResponse>> ListAsync(CancellationToken ct)
    {
        var list = await _repo.ListWithItemsAsync(ct);
        return list.Select(p => new PurchaseOrderListResponse(
            p.UUID, p.SupplierId, p.WarehouseId, p.ExpectedDate, p.Notes, p.PurchaseOrderItems.Count)).ToList();
    }

    public async Task<PurchaseOrderDetailResponse?> GetAsync(Guid id, CancellationToken ct)
    {
        var po = await _repo.GetWithItemsAsync(id, ct);
        if (po is null) return null;

        return new PurchaseOrderDetailResponse(
            po.UUID,
            po.SupplierId,
            po.WarehouseId,
            po.ExpectedDate,
            po.Notes,
            po.PurchaseOrderItems.Select(i => new PurchaseOrderDetailItem(i.ProductId, i.Quantity, i.UnitCost)).ToList()
        );
    }
}
