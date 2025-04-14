using EventApp.Application.Concrete;
using EventApp.Domain.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<LoginDTO> _loginValidator;
        private readonly IValidator<UserDTO> _registerValidator;
        public AuthController(IAuthService authService, IValidator<LoginDTO> loginValidator, IValidator<UserDTO> registerValidator)
        {
            _authService = authService;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var validationResult = await _loginValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = await _authService.LoginAsync(dto);

            if (!result.Success)
                return NotFound(result.Message);

            var token = result.Data;

            return Ok(token);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO dto)
        {
            var validationResult = await _registerValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = await _authService.RegisterAsync(dto);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
