using System.Data;
using Axolotl.Application.Interfaces.Persistence;

namespace Axolotl.Application.Interfaces.Services;

public interface IUnitOfWork : IDisposable
{
    IProductPriceHistoryRepository ProductPriceHistory { get; }
    ISalesRepository Sales { get; }

    Task SaveChangesAsync();
    IDbTransaction BeginTransaction();
}