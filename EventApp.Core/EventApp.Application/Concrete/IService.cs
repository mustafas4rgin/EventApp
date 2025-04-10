namespace EventApp.Application.Concrete;

public interface IService<T> where T : class
{
    IServiceResult<IQueryable<T>> GetAll();
    Task<IServiceResult<T>> GetByIdAsync(int id);
    Task<IServiceResult> CreateAsync(T entity);
    Task<IServiceResult> UpdateAsync(T entity);
    Task<IServiceResult> DeleteAsync(int id);
}