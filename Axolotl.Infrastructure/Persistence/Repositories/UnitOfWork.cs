using Axolotl.Domain.Entities;
using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Infrastructure.Persistence.Context;
using Axolotl.Application.Interfaces.Services;

//I don't actually know if I'm doing this right
namespace Axolotl.Infrastructure.Persistence.Repositories
{
    // public class UnitOfWork : IUnitOfWork
    // {
    //     private readonly ApplicationDbContext _context;

    //     public UnitOfWork(ApplicationDbContext context)
    //     {
    //         _context = context;
    //     }

    //     //This for the entities we have so far
    //     public IGenericRepository<Category> Categories => _categories ??= new GenericRepository<Category>(_context);
    //     private IGenericRepository<Category>? _categories;

    //     //-----------------------------------------------------------
    //     public async Task<int> SaveAsync()
    //     {
    //         return await _context.SaveChangesAsync();
    //     }

    //     public void Dispose()
    //     {
    //         _context.Dispose();
    //     }
    // }
}