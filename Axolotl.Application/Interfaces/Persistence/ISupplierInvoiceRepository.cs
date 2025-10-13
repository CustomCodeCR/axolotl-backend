
using Axolotl.Domain.Entities;

namespace Axolotl.Application.Interfaces.Persistence;

public interface ISupplierInvoiceRepository : IGenericRepository<SupplierInvoices>
{
    Task<IReadOnlyList<SupplierInvoices>> ListAsync(CancellationToken ct);
    Task<SupplierInvoices?> GetWithItemsAsync(Guid uuid, CancellationToken ct);
}
