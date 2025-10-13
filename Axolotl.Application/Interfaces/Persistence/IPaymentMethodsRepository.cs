using Axolotl.Domain.Entities;

namespace Axolotl.Application.Interfaces.Persistence
{
    public interface IPaymentMethodsRepository
    {
        Task<PaymentMethods?> GetByIdAsync(Guid uuid, CancellationToken ct = default);
        Task<PaymentMethods?> GetByCodeAsync(string code, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid uuid, CancellationToken ct = default);
        Task<List<PaymentMethods>> GetAllAsync(CancellationToken ct = default);
    }
}
