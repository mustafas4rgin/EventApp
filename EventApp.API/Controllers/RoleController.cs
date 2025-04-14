using System.ComponentModel.DataAnnotations;
using AutoMapper;
using EventApp.Application.Concrete;
using EventApp.Core.Services;
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
    public class RoleController : GenericController<Role,RoleDTO,UpdateRoleDTO>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public RoleController(IValidator<RoleDTO> createValidator,IValidator<UpdateRoleDTO> updateValidator, IMapper mapper, IRoleService roleService) : base(roleService,mapper,createValidator,updateValidator){
            _mapper = mapper;
            _roleService = roleService;
        }
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetAllWithUsers()
        {
            var result = await _roleService.GetRolesWithUsers();

            if(!result.Success)
                return NotFound(result.Message);

            var roles = result.Data;

            var dto = _mapper.Map<List<RoleListDTO>>(roles);

            return Ok(dto);
        }
        [HttpGet("GetRole/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _roleService.GetRoleWithUser(id);

            if (!result.Success)
                return NotFound(result.Message);

            var role = result.Data;

            var dto = _mapper.Map<RoleListDTO>(role);

            return Ok(dto);
        }
    }
}
