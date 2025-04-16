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
    public class UserEventRelController : GenericController<EventUserRel,EventuserRelDTO,EventuserRelDTO>
    {
        private readonly IUserEventRel _eventUserRelService;
        public UserEventRelController(IUserEventRel service, IValidator<EventuserRelDTO> createValidator, IValidator<EventuserRelDTO> updateValidator,IMapper mapper)
        : base(service,mapper,createValidator,updateValidator)
        {
            _eventUserRelService = service;
        }
        [HttpDelete("Delete/{userId}/{eventId}")]
        public async Task<IActionResult> DeleteUserEventRel(int userId, int eventId)
        {
            var result = await _eventUserRelService.DeleteEventUserRelAsync(eventId, userId);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }
        [HttpPost("Create/{userId}/{eventId}")]
        public async Task<IActionResult> CreateUserEventRel(int userId, int eventId)
        {
            var result = await _eventUserRelService.CreateEventUserRelAsync(eventId, userId);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }
    }
}
