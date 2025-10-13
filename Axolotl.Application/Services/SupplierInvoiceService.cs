using Axolotl.Application.Dtos.SupplierInvoices;
using Axolotl.Application.Dtos.SupplierInvoices.Requests;
using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Domain.Entities;

namespace Axolotl.Application.Services;

public class SupplierInvoiceService : ISupplierInvoiceService
{
    private readonly ISupplierInvoiceRepository _repo;
    private readonly IUnitOfWork _uow;

    public SupplierInvoiceService(ISupplierInvoiceRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    // LISTA (usa record con constructor)
    public async Task<IReadOnlyList<SupplierInvoiceListResponse>> ListAsync(CancellationToken ct)
    {
        var list = await _repo.ListAsync(ct);

        return list
            .Select(i => new SupplierInvoiceListResponse(
                i.UUID,
                i.InvoiceNumber,
                i.TotalIncTax))
            .ToList();
    }

    // DETALLE (usa record con constructor)
    public async Task<SupplierInvoiceDetailResponse?> GetAsync(Guid id, CancellationToken ct)
    {
        var inv = await _repo.GetWithItemsAsync(id, ct);
        if (inv is null) return null;

        var items = inv.SupplierInvoiceItems
            .Select(x => new SupplierInvoiceDetailItem(
                x.ProductId,
                x.Quantity,
                x.UnitCost,
                x.DiscountPct,
                x.Description
            ))
            .ToList();

        return new SupplierInvoiceDetailResponse(
            inv.UUID,
            inv.InvoiceNumber,
            inv.TotalExTax,
            inv.TotalTax,
            inv.TotalIncTax,
            items
        );
    }

  
    public async Task<Guid> CreateAsync(CreateSupplierInvoiceDto dto, CancellationToken ct)
    {
        
        await Task.CompletedTask;

        // Si quieres implementar ya mismo:
        // var entity = new SupplierInvoices
        // {
        //     SupplierId     = dto.SupplierId,
        //     WarehouseId    = dto.WarehouseId,
        //     InvoiceNumber  = dto.InvoiceNumber,
        //     TotalExTax     = dto.TotalExTax,
        //     TotalTax       = dto.TotalTax,
        //     TotalIncTax    = dto.TotalIncTax,
        //     SupplierInvoiceItems = dto.Items.Select(it => new SupplierInvoiceItems
        //     {
        //         ProductId   = it.ProductId,
        //         Quantity    = it.Quantity,
        //         UnitCost    = it.UnitCost,
        //         DiscountPct = it.DiscountPct,
        //         Description = it.Description
        //     }).ToList()
        // };

        // await _repo.CreateAsync(entity);
        // await _uow.SaveChangesAsync(ct);
        // return entity.UUID;

        return Guid.Empty;
    }
}
