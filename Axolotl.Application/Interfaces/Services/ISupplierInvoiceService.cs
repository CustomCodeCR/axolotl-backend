using Axolotl.Application.Dtos.SupplierInvoices;
using Axolotl.Application.Dtos.SupplierInvoices.Requests;
using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Domain.Entities;

namespace Axolotl.Application.Interfaces.Services;

public interface ISupplierInvoiceService
{
    Task<Guid> CreateAsync(CreateSupplierInvoiceDto dto, CancellationToken ct);
    Task<IReadOnlyList<SupplierInvoiceListResponse>> ListAsync(CancellationToken ct);
    Task<SupplierInvoiceDetailResponse?> GetAsync(Guid id, CancellationToken ct);
}
