
using AutoMapper;
using EventApp.Application.Concrete;
using EventApp.Data.Context;
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
        private readonly IValidator<UpdateUserDTO> _updateValidator;
        private readonly IMapper _mapper;
        public UserController( IValidator<UpdateUserDTO> updateValidator, IMapper mapper, IUserService userService)
        {
            _updateValidator = updateValidator;
            _mapper = mapper;
            _userService = userService;
        }
        [HttpPut("Update/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, UpdateUserDTO dto)
        {
            var userResult = await _userService.GetByIdAsync(userId);

            if (!userResult.Success)
                return NotFound(userResult.Message);

            var validationResult = await _updateValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = userResult.Data;

            _mapper.Map(dto, user);

            var updateResult = await _userService.UpdateUserAsync(user);

            if (!updateResult.Success)
                return BadRequest(updateResult.Message);
            
            return Ok(updateResult.Message);
        }
        [HttpDelete("Delete/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await _userService.DeleteUserAsync(userId);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUser(int userId, string? include)
        {
            var result = await _userService.GetUserWithIncludeAsync(userId, include);

            if (!result.Success)
                return NotFound(result.Message);

            var user = result.Data;

            var dto = _mapper.Map<UsersDTO>(user);

            return Ok(dto);
        }
        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers([FromQuery] string? include)
        {
            var result = await _userService.GetUsersWithIncludesAsync(include);

            if (!result.Success)
                return NotFound(result.Message);

            var users = result.Data;

            var dto = _mapper.Map<List<UsersDTO>>(users);

            return Ok(dto);
        }
    }
}
