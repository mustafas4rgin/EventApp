using System.ComponentModel.DataAnnotations;
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
    public class RoleController : GenericController<Role,RoleDTO,UpdateRoleDTO>
    {
        public RoleController(IValidator<RoleDTO> createValidator,IValidator<UpdateRoleDTO> updateValidator, IMapper mapper, IService<Role> service) : base(service,mapper,createValidator,updateValidator){}
    }
}
