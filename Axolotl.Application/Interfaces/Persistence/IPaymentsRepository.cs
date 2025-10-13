using Axolotl.Domain.Entities;

namespace Axolotl.Application.Interfaces.Persistence
{
    public interface IPaymentsRepository
    {
        Task AddAsync(Payments entity, CancellationToken ct = default);
        Task UpdateAsync(Payments entity, CancellationToken ct = default);
        Task<Payments?> GetByIdAsync(Guid paymentUuid, CancellationToken ct = default);
        Task<List<Payments>> GetByInvoiceAsync(Guid invoiceUuid, CancellationToken ct = default);
        Task<decimal> GetTotalCompletedByInvoiceAsync(Guid invoiceUuid, CancellationToken ct = default);
    }
}
