using System.ComponentModel.DataAnnotations;
using AutoMapper;
using EventApp.Core.Concrete;
using EventApp.Data.DTOs;
using EventApp.Data.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : GenericController<Role,RoleDTO,UpdateRoleDTO>
    {
        public RoleController(IValidator<RoleDTO> createValidator,IValidator<UpdateRoleDTO> updateValidator, IMapper mapper, IService<Role> service) : base(service,mapper,createValidator,updateValidator){}
    }
}
