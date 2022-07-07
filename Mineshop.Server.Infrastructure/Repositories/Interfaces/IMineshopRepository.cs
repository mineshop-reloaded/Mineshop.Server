using System.Linq.Expressions;
using Mineshop.Server.Domain.Entity;

namespace Infrastructure.Repositories.Interfaces;

public interface IMineshopRepository<T> where T : MineshopEntity
{
    Task<T?> GetByIdentifier(Guid identifier);
    Task<List<T>> GetAll();
    Task<T> Create(T entity);
    Task<T> Update(T entity);
    Task<T?> Delete(Guid identifier);
    Task<bool> Contains(Expression<Func<T, bool>> predicate);
    IQueryable<T> Queryable();
}