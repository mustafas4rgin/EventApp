using AutoMapper;
using EventApp.API.Controllers;
using EventApp.Application.Concrete;
using EventApp.Domain.DTOs;
using EventApp.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : GenericController<Event,EventDTO,UpdateEventDTO>
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        public EventController(IEventService eventService,IMapper mapper, IValidator<EventDTO> createValidator, IValidator<UpdateEventDTO> updateValidator) : base(eventService,mapper,createValidator,updateValidator){
            _eventService = eventService;
            _mapper = mapper;
        }
        [HttpGet("events-with-users")]
        public async Task<IActionResult> GetEventsWithBookedUsers()
        {
            var result = await _eventService.GetEventsWithBookedUsers();

            if(!result.Success)
                return NotFound(result.Message);

            var events = result.Data;

            var dto = _mapper.Map<List<EventsWithUsersDTO>>(events);

            return Ok(dto);
        }
        [HttpGet("events-with-createduser")]
        public async Task<IActionResult> GetEventsWithCreatedUser()
        {
            var result = await _eventService.GetEventsWithCreator();

            if(!result.Success)
                return NotFound(result.Message);

            var events = result.Data;

            var dto = _mapper.Map<List<GetEventsWithCreatedUserDTO>>(events);

            return Ok(dto);
        }
        
    }
}
