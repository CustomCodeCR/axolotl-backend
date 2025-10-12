using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Axolotl.Infrastructure.Persistence.Context;
using DocumentFormat.OpenXml.Bibliography;
using System.Data;
using System.Reflection.PortableExecutable;

namespace Axolotl.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private bool _disposed = false;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public IDbTransaction BeginTransaction()
    {
        var transaction = _context.Database.BeginTransaction();
        return transaction.GetDbTransaction();
    }
}