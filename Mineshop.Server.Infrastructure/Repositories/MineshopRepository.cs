using System.Linq.Expressions;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Mineshop.Server.Domain.Entity;

namespace Infrastructure.Repositories;

public class MineshopRepository<T> : IMineshopRepository<T> where T : MineshopEntity
{
    protected readonly MineshopContext Context;

    protected MineshopRepository(MineshopContext context)
    {
        Context = context;
    }

    public virtual async Task<T?> GetByIdentifier(Guid identifier)
    {
        return await Context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Identifier == identifier);
    }

    public virtual async Task<List<T>> GetAll()
    {
        return await Context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual async Task<T> Create(T entity)
    {
        await Context.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T> Update(T entity)
    {
        Context.Update(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T?> Delete(Guid identifier)
    {
        var mineshopEntity = await GetByIdentifier(identifier);
        if (mineshopEntity == null) return null;

        Context.Remove(mineshopEntity);
        await Context.SaveChangesAsync();
        return mineshopEntity;
    }

    public Task<bool> Contains(Expression<Func<T, bool>> predicate)
    {
        return Context.Set<T>()
            .AsNoTracking()
            .AnyAsync(predicate);
    }

    public IQueryable<T> Queryable()
    {
        return Context.Set<T>()
            .AsQueryable();
    }
}