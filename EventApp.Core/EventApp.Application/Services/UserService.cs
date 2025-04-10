using EventApp.Application.Concrete;
using EventApp.Application.Helpers;
using EventApp.Application.Results;
using EventApp.Domain.DTOs;
using EventApp.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Application.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;
    private readonly IValidator<User> _validator;
    public UserService(IRepository<User> repository,IValidator<User> validator)
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task<IServiceResult<IEnumerable<User>>> GetAllUsersWithRoleAsync()
    {
        var users = await _repository.GetAll()
                    .Include(u => u.Role)
                    .ToListAsync();

        if(!users.Any() || users.Count() <= 0)
            return new ErrorResult<IEnumerable<User>>("There is no user.");

        return new SuccessResult<IEnumerable<User>>("Users:",users);
    }
    public async Task<IServiceResult<IEnumerable<User>>> GetAllUsersWithBookedEventsAsync()
    {
        var users = await _repository.GetAll()
                    .Include(u => u.BookedEvents)
                    .ToListAsync();
        
        if(!users.Any() || users.Count() <= 0)
            return new ErrorResult<IEnumerable<User>>("There is no user.");

        return new SuccessResult<IEnumerable<User>>("Users:",users);
    }
    public async Task<IServiceResult<User>> LoginAsync(LoginDTO dto)
    {
        var user = await _repository.GetAll().FirstOrDefaultAsync(u => u.Email == dto.Email);

        if(user is null)
            return new ErrorResult<User>("User not found.");           
        
        return new SuccessResult<User>("User found",user);
    }
    public async Task<IServiceResult> RegisterAsync(UserDTO dto)
    {
        var existingUser = await _repository.GetAll().FirstOrDefaultAsync(u => u.Email == dto.Email || u.Username == dto.Username);

        if(existingUser is not null)
            return new RawErrorResult("There is a user with this email or username.");

        var newUser = MappingHelper.UserMap(dto);

        var validationResult = await _validator.ValidateAsync(newUser);

        if(!validationResult.IsValid)
            return new RawErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

        await _repository.AddAsync(newUser);
        await _repository.SaveChangesAsync();

        return new RawSuccessResult("User registered successfully.");
    }
}