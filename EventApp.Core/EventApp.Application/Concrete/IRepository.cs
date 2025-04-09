
namespace EventApp.Application.Concrete;

public interface IRepository<T> where T : class
{
    Task SaveChangesAsync();
    IQueryable<T> GetAll();
    Task<T> GetByIdAsync(int id);
    Task UpdateAsync(T entity);
    Task AddAsync(T entity);
    Task DeleteByIdAsync(int id);
}