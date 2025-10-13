using System.Data;
using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Axolotl.Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public IProductPriceHistoryRepository ProductPriceHistory { get; }
        public ISalesRepository Sales { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IProductPriceHistoryRepository productPriceHistory,
            ISalesRepository sales)
        {
            _context = context;
            ProductPriceHistory = productPriceHistory;
            Sales = sales;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public IDbTransaction BeginTransaction()
        {
            var efTx = _context.Database.BeginTransaction();
            return efTx.GetDbTransaction();
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
    }
}
