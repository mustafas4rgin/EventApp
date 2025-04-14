using AutoMapper;
using EventApp.API.Controllers;
using EventApp.Application.Concrete;
using EventApp.Application.Results;
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
    public class EventController : GenericController<Event, EventDTO, UpdateEventDTO>
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        public EventController(IEventService eventService, IMapper mapper, IValidator<EventDTO> createValidator, IValidator<UpdateEventDTO> updateValidator) : base(eventService, mapper, createValidator, updateValidator)
        {
            _eventService = eventService;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] string? include)
        {
            var result =await _eventService.GetByIdWithIncludesAsync(id,include);

            if(!result.Success)
                return NotFound(result.Message);

            var eventEntity = result.Data;

            var dto = _mapper.Map<EventsDTO>(eventEntity);

            return Ok(dto);
        }
        [HttpGet("get-events")]
        public  async Task<IActionResult> GetAll([FromQuery]string? include)
        {
            var result = await _eventService.GetEventsWithIncludesAsync(include);

            if(!result.Success)
                return NotFound(result.Message);

            var events = result.Data;

            var dto = _mapper.Map<List<EventsDTO>>(events);

            return Ok(dto);
        }

    }
}
