using EzShare.Application.Contracts.Repositories;
using EzShare.Domain.Common;
using EzShare.Domain.Entities;
using EzShare.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EzShare.Infrastructure.Repositories;

public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T>
    where T : BaseEntity
{
    public async Task<T?> GetByIdAsync(Guid id)
    { 
        return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);  
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        var entry = await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task UpdateAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
    }
}