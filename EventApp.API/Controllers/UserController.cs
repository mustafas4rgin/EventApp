
using AutoMapper;
using EventApp.Application.Concrete;
using EventApp.Domain.DTOs;
using EventApp.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserDTO> _registerValidator;
        private readonly IValidator<LoginDTO> _loginValidator;
        private readonly IMapper _mapper;
        public UserController(IMapper mapper, IUserService userService,IValidator<LoginDTO> loginValidator, IValidator<UserDTO> registerValidator)
        {
            _mapper = mapper;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _userService = userService;
        }
        [HttpGet("users-with-role")]
        public async Task<IActionResult> GetUsersWitRole()
        {
            var result = await _userService.GetAllUsersWithRoleAsync();
            
            if(!result.Success)
                return NotFound(result.Message);

            var users = result.Data;

            var dto = _mapper.Map<List<UserWithRoleDTO>>(users);

            return Ok(dto);
        }
        [HttpGet("users-with-events")]
        public async Task<IActionResult> GetUsersWithEvents()
        {
            var result = await _userService.GetAllUsersWithBookedEventsAsync();

            if(!result.Success)
                return NotFound(result.Message);

            var users = result.Data;

            var dto = _mapper.Map<List<UserWithEventsDTO>>(users);

            return Ok(dto);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var validationResult = await _loginValidator.ValidateAsync(dto);

            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = await _userService.LoginAsync(dto);

            if(!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO dto)
        {
            var validationResult = await _registerValidator.ValidateAsync(dto);

            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = await _userService.RegisterAsync(dto);

            if(!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
