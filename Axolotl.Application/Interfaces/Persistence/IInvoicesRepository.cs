using Axolotl.Domain.Entities;

namespace Axolotl.Application.Interfaces.Persistence
{
    public interface IInvoicesRepository
    {
        Task AddAsync(Invoices invoice, CancellationToken ct = default);
        Task<bool> ExistsNumberAsync(string invoiceNumber, CancellationToken ct = default);
        Task<Invoices?> GetWithItemsAsync(Guid invoiceUuid, CancellationToken ct = default);
        Task<List<Invoices>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct = default);
    }
}
