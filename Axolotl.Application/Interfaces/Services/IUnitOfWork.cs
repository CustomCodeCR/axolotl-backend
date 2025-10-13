using System.Data;
using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Domain.Entities;

namespace Axolotl.Application.Interfaces.Services;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync();
    IDbTransaction BeginTransaction();

    //Repos
    IGenericRepository<Category> Category { get; }
}