using EventApp.Core.Concrete;
using EventApp.Data.Context;
using EventApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Data.Repository;

public class Repository<T> : IRepository<T> where T : EntityBase
{
    private readonly AppDbContext context;
    private readonly DbSet<T> dbSet;

    public Repository(AppDbContext context)
    {
        this.context = context;
        this.dbSet = context.Set<T>();
    }
    public IQueryable<T> GetAll()
    {
        return dbSet;
    }
    public async Task<T> GetByIdAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }
    public async Task AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await dbSet.AddAsync(entity);
    }
    public Task UpdateAsync(T entity)
    {
        dbSet.Update(entity);
        entity.UpdatedAt = DateTime.UtcNow;
        return Task.CompletedTask;
    }
    public async Task DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is not null)
        {
            dbSet.Remove(entity);
        }
    }
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}