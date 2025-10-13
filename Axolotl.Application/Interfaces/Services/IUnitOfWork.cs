using System.Data;
using Axolotl.Application.Interfaces.Persistence;

namespace Axolotl.Application.Interfaces.Services;

public interface IUnitOfWork : IDisposable
{
    IProductPriceHistoryRepository ProductPriceHistory { get; }
    ISalesRepository Sales { get; }
    IInvoicesRepository Invoices { get; }
    IPaymentsRepository Payments { get; }
    IPaymentMethodsRepository PaymentMethods { get; }

    Task SaveChangesAsync();
    IDbTransaction BeginTransaction();
}