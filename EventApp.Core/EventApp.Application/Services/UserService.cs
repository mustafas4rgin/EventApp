using EventApp.Application.Concrete;
using EventApp.Application.Helpers;
using EventApp.Application.Results;
using EventApp.Core.Services;
using EventApp.Domain.DTOs;
using EventApp.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Application.Services;

public class UserService :Service<User>, IUserService
{
    private readonly IRepository<User> _repository;
    private readonly IValidator<User> _validator;
    public UserService(IRepository<User> repository, IValidator<User> validator) : base(repository,validator)
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task<IServiceResult> DeleteUserAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        
        if (user is null)
           return new RawErrorResult($"There is no user with ID: {id}");

        await _repository.DeleteByIdAsync(id);
        await _repository.SaveChangesAsync();

        return new RawSuccessResult("User deleted successfully.");
    }
    public async Task<IServiceResult<IEnumerable<User>>> GetUsersWithIncludesAsync(string? include)
    {
        try
        {
            var query = _repository.GetAll();

            if (!string.IsNullOrWhiteSpace(include))
            {
                var includes = include.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var inc in includes.Select(x => x.Trim().ToLower()))
                {
                    if (inc == "role")
                        query = query.Include(e => e.Role);
                    else if (inc == "bookedevents")
                        query = query.Include(e => e.BookedEvents);
                }
            }

            var eventEntity = await query.ToListAsync();

            if (eventEntity == null)
                return new ErrorResult<IEnumerable<User>>("No user found.");

            return new SuccessResult<IEnumerable<User>>("Users found.", eventEntity);
        }

        catch (Exception ex)
        {
            return new ErrorResult<IEnumerable<User>>(ex.Message);
        }
    }
    public async Task<IServiceResult> UpdateUserAsync(User user)
    {
        if (user is null)
            return new ErrorResult<User>("User cannot be null.");

        var validationResult = await _validator.ValidateAsync(user);

        if (!validationResult.IsValid)
            return new ErrorResult<User>(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

        await _repository.UpdateAsync(user);
        await _repository.SaveChangesAsync();

        return new SuccessResult<User>("User updated successfully.",user);
    }
    public async Task<IServiceResult<User>> GetUserWithIncludeAsync(int id, string? include)
    {
        try
        {
            var query = _repository.GetAll();

            if (!string.IsNullOrWhiteSpace(include))
            {
                var includes = include.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var inc in includes.Select(x => x.Trim().ToLower()))
                {
                    if (inc == "role")
                        query = query.Include(e => e.Role);
                    else if (inc == "bookedevents")
                        query = query.Include(e => e.BookedEvents);
                }
            }

            var userEntity = await query.FirstOrDefaultAsync(u => u.Id == id);

            if (userEntity == null)
                return new ErrorResult<User>("No user found.");

            return new SuccessResult<User>("User found.", userEntity);
        }
        catch (Exception ex)
        {
            return new ErrorResult<User>(ex.Message);
        }
    }
}