using System.Data;

namespace Axolotl.Application.Interfaces.Services;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync();
    IDbTransaction BeginTransaction();
}