using EventApp.Application.Concrete;
using EventApp.Application.Results;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Core.Services;

public class Service<T> : IService<T> 
    where T : class
{
    private readonly IValidator<T> validator;
    private readonly IRepository<T> repository;
    public Service(IRepository<T> repository,IValidator<T> validator)
    {
        this.validator = validator;
        this.repository = repository;
    }
    public async Task<IServiceResult<IEnumerable<T>>> GetAllAsync()
    {
        var entities = await repository.GetAll().ToListAsync();

        if (entities == null || entities.Count == 0)
            return new ErrorResult<IEnumerable<T>>("There is no data.");

        return new SuccessResult<IEnumerable<T>>("Data found.",entities);
    }
    public async Task<IServiceResult<T>> GetByIdAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);

        if(entity is null)
            return new ErrorResult<T>("There is no entity.");

        return new SuccessResult<T>("Entity found.",entity);
    }
    public async Task<IServiceResult> CreateAsync(T entity)
    {
        if (entity is null)
            return new RawErrorResult("Error while adding entity.");

        var validationResult = await validator.ValidateAsync(entity);
        if(!validationResult.IsValid)
            return new RawErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

        await repository.AddAsync(entity);
        await repository.SaveChangesAsync();

        return new RawSuccessResult("Entity added successfully.");
    }
    public async Task<IServiceResult> UpdateAsync(T entity)
    {
        if (entity is null)
            return new RawErrorResult("Error while updating entity.");

        var validationResult = await validator.ValidateAsync(entity);

        if(!validationResult.IsValid)
            return new RawErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

        await repository.UpdateAsync(entity);
        await repository.SaveChangesAsync();

        return new RawSuccessResult("Entity updated successfully.");
    }
    public async Task<IServiceResult> DeleteAsync(int id)
    {
        var result = await GetByIdAsync(id);

        if (!result.Success)
            return new RawErrorResult("An error occured while deleting entity.");

        await repository.DeleteByIdAsync(id);
        await repository.SaveChangesAsync();

        return new RawSuccessResult("Entity deleted successfully.");
    }
}