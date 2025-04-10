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
        public UserEventRelController(IService<EventUserRel> service, IValidator<EventuserRelDTO> createValidator, IValidator<EventuserRelDTO> updateValidator,IMapper mapper)
        : base(service,mapper,createValidator,updateValidator)
        {}
    }
}
