using AutoMapper;
using EventApp.Core.Concrete;
using EventApp.Data.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T, TDto, TUpdateDto> : ControllerBase
    where T : class
    where TDto : class
    where TUpdateDto : class
    {
        private readonly IService<T> _service;
        private readonly IValidator<TDto> _createValidator;
        private readonly IValidator<TUpdateDto> _updateValidator;
        private readonly IMapper _mapper;
        public GenericController(IService<T> service, IMapper mapper, IValidator<TDto> createValidator,IValidator<TUpdateDto> updateValidator)
        {
            _updateValidator = updateValidator;
            _createValidator = createValidator;
            _mapper = mapper;
            _service = service;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            if (!result.Success)
                return NotFound(result.Message);

            var entities = result.Data;

            return Ok(entities);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.Success)
                return NotFound(result.Message);

            var entity = result.Data;

            return Ok(entity);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(TDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);

            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var entity = _mapper.Map<T>(dto);

            if(entity is IEntityBase entityBase)
                entityBase.CreatedAt = DateTime.UtcNow;

            var result = await _service.CreateAsync(entity);

            if(!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(TUpdateDto dto, int id)
        {
            var entityResult = await _service.GetByIdAsync(id);

            if(!entityResult.Success)
                return NotFound(entityResult.Message);

            var entity = entityResult.Data;
    
            var validationResult = await _updateValidator.ValidateAsync(dto);

            if(!validationResult.IsValid)
               return BadRequest(validationResult.Errors);

            entity = _mapper.Map(dto, entity);

            if (entity is IEntityBase entityBase)
            {
                entityBase.Id = id;
                entityBase.UpdatedAt = DateTime.UtcNow;
            }

            var updateResult = await _service.UpdateAsync(entity);

            if(!updateResult.Success)
                return BadRequest(updateResult.Message);

            return Ok(updateResult.Message);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if(!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }
    }
}
