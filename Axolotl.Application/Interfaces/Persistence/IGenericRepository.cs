using Axolotl.Domain.Entities;

namespace Axolotl.Application.Interfaces.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAllQueryable();
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetSelectAsync();
    Task<T> GetByUUIDAsync(Guid uuid);
    Task CreateAsync(T entity);
    void UpdateAsync(T entity);
    Task DeleteAsync(Guid uuid);
    Task<bool> ExecAsync(string storedProcedure, object parameters);
}