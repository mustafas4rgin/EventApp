using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventApp.Application.Concrete;
using EventApp.Application.Helpers;
using EventApp.Application.Results;
using EventApp.Domain.DTOs;
using EventApp.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EventApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _repository;
    private readonly IValidator<User> _validator;
    public AuthService(IValidator<User> validator, IRepository<User> repository)
    {
        _validator = validator;
        _repository = repository;
    }
    public async Task<IServiceResult> RegisterAsync(UserDTO dto)
    {
        var existingUser = await _repository.GetAll()
                            .FirstOrDefaultAsync(u => u.Email == dto.Email || u.Username == dto.Username);

        if (existingUser is not null)
            return new RawErrorResult("There is a user with that username or email.");

        var newUser = MappingHelper.UserMap(dto);

        var validationResult = await _validator.ValidateAsync(newUser);

        if (!validationResult.IsValid)
            return new RawErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));
        
        await _repository.AddAsync(newUser);
        await _repository.SaveChangesAsync();

        return new RawSuccessResult("User registered successfully.");
    }
    public async Task<IServiceResult<string>> LoginAsync(LoginDTO dto)
    {
        var user = await _repository.GetAll()
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user is null)
            return new ErrorResult<string>("There is no user with this email.");

        if(!HashingHelper.VerifyPasswordHash(dto.Password,user.PasswordHash,user.PasswordSalt))
            return new ErrorResult<string>("Wrong password.");

        var tokenResult = GenerateJwtToken(user);

        if (!tokenResult.Success)
            return new ErrorResult<string>("Error.");

        return new SuccessResult<string>("Token:",tokenResult.Data);
    }
    public IServiceResult<string> GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HayatimdakiEnGuvenliAnahtarBuOlsaGerek230723"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.name)
        };

        var token = new JwtSecurityToken(
            issuer: "groupapp.com",
            audience: "groupapp.com",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        string tokenn = new JwtSecurityTokenHandler().WriteToken(token);
        return new SuccessResult<string>("token:",tokenn);
    }
}