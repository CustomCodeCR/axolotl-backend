using Axolotl.Application.Commons.Bases;

namespace Axolotl.Application.Interfaces.Services;

public interface IOrderingQuery
{
    IQueryable<T> Ordering<T>(BasePagination request, IQueryable<T> queryable) where T : class;
}