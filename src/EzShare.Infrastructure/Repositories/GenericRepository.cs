using EzShare.Application.Contracts.Repositories;
using EzShare.Domain.Common;
using EzShare.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EzShare.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Context;

    public GenericRepository(AppDbContext context) => Context = context;

    public async Task<T?> GetByIdAsync(int id)
    { 
        return await Context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);  
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await Context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        var entry = await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task UpdateAsync(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();
    }
}