using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Domain.Entities;
using Axolotl.Domain.Enums;
using Axolotl.Infrastructure.Persistence.Context;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Axolotl.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entity;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _entity = context.Set<T>();
    }

    public IQueryable<T> GetAllQueryable()
    {
        var response = _entity
            .Where(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null);
        return response;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var response = await _entity
            .Where(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null)
        .AsNoTracking()
        .ToListAsync();
        return response;
    }

    public async Task<IEnumerable<T>> GetSelectAsync()
    {
        var getAll = await _entity
            .Where(x => x.State.Equals(StateType.Active) && x.AuditDeleteUser == null && x.AuditDeleteDate == null)
            .AsNoTracking()
            .ToListAsync();

        return getAll;
    }

    public async Task<T> GetByUUIDAsync(Guid uuid)
    {
        var response = await _entity
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.UUID == uuid &&
            x.AuditDeleteUser == null && x.AuditDeleteDate == null);
        return response!;
    }

    public async Task CreateAsync(T entity)
    {
        entity.AuditCreateUser = 1;
        entity.AuditCreateDate = DateTime.UtcNow;
        await _context.AddAsync(entity);
    }

    public void UpdateAsync(T entity)
    {
        entity.AuditUpdateUser = 1;
        entity.AuditUpdateDate = DateTime.UtcNow;
        _context.Update(entity);
        _context.Entry(entity).Property(x => x.AuditCreateUser).IsModified = false;
        _context.Entry(entity).Property(x => x.AuditCreateDate).IsModified = false;
    }

    public async Task DeleteAsync(Guid uuid)
    {
        T entity = await GetByUUIDAsync(uuid);
        entity.AuditDeleteUser = 1;
        entity.AuditDeleteDate = DateTime.UtcNow;
        _context.Update(entity);
    }

    public async Task<bool> ExecAsync(string storedProcedure, object parameters)
    {
        using var connection = _context.CreateConnection;
        await connection.OpenAsync();

        try
        {
            var objParam = new DynamicParameters(parameters);
            var recordAffected = await connection.ExecuteAsync(
                storedProcedure,
                param: objParam,
                commandType: CommandType.StoredProcedure
            );

            if (recordAffected > 0)
            {
                return true;
            }

            foreach (var param in objParam.ParameterNames)
            {
                var paramValue = objParam.Get<object>(param);
                if (paramValue != null)
                {
                    return true;
                }
            }

            return false;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }
}