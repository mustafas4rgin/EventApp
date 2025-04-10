using EventApp.Application.Concrete;
using EventApp.Application.Results;
using EventApp.Core.Services;
using EventApp.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Application.Services;

public class RoleService : Service<Role>, IRoleService
{
    private readonly IRepository<Role> _repository;
    private readonly IValidator<Role> _validator;
    public RoleService(IRepository<Role> repository, IValidator<Role> validator) : base(repository,validator)
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task<IServiceResult<IEnumerable<Role>>> GetRolesWithUsers()
    {
        var roles = await _repository.GetAll()
                    .Include(r => r.Users)
                    .ToListAsync();

        if(!roles.Any() || roles.Count() <= 0)
            return new ErrorResult<IEnumerable<Role>>("There is no role.");

        return new SuccessResult<IEnumerable<Role>>("Roles:",roles);
    }
}