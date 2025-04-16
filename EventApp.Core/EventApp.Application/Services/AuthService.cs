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
    private readonly IEmailService _emailService;
    public AuthService(IEmailService emailService, IValidator<User> validator, IRepository<User> repository)
    {
        _emailService = emailService;
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
    public async Task<IServiceResult<User>> LoginAsync(LoginDTO dto)
    {
        var user = await _repository.GetAll()
                            .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user is null)
            return new ErrorResult<User>("There is no user with this email.");

        if (!HashingHelper.VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
            return new ErrorResult<User>("Wrong password.");

        var tokenResult = GenerateJwtToken(user);

        if (!tokenResult.Success)
            return new ErrorResult<User>("Error.");

        return new SuccessResult<User>(tokenResult.Data, user);
    }
    public IServiceResult<string> GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HayatimdakiEnGuvenliAnahtarBuOlsaGerek230723"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("id", user.Id.ToString()),
        new Claim("username", user.Username),  
        new Claim("email", user.Email),  
        new Claim("phone", user.Phone),  
        new Claim("name", user.Name),  
        new Claim("role", user.Role.name) 
    };

        var token = new JwtSecurityToken(
            issuer: "eventapp.com",
            audience: "eventapp.com",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        // UTF-8 kodlamasında token oluşturma
        string tokenn = new JwtSecurityTokenHandler().WriteToken(token);
        return new SuccessResult<string>("token:", tokenn);
    }
    public async Task<IServiceResult> ForgotPasswordAsync(string email)
    {
        var user = await _repository.GetAll()
                            .FirstOrDefaultAsync(u => u.Email == email);

        if (user is null)
            return new RawErrorResult("There is no user with this email.");

        var resetToken = Guid.NewGuid().ToString();

        user.ValidationToken = resetToken;
        user.TokenExpiration = DateTime.UtcNow.AddHours(1);

        await _repository.UpdateAsync(user);
        await _repository.SaveChangesAsync();

        var result = await _emailService.ResetPasswordAsync(email, resetToken);

        if (!result.Success)
            return new RawErrorResult("Error sending email.");

        return new RawSuccessResult("Reset password email sent successfully.");
    }
    public async Task<IServiceResult> ResetPasswordAsync(string token, string newPassword)
    {
        var user = await _repository.GetAll()
                            .FirstOrDefaultAsync(u => u.ValidationToken == token);

        if (user is null)
            return new RawErrorResult("Invalid token.");

        if (user.TokenExpiration < DateTime.UtcNow)
            return new RawErrorResult("Token expired.");

        HashingHelper.CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        user.ValidationToken = string.Empty;
        user.TokenExpiration = DateTime.MinValue;

        await _repository.UpdateAsync(user);
        await _repository.SaveChangesAsync();

        return new RawSuccessResult("Password reset successfully.");
    }
}