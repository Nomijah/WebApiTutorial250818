using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiTutorial250818.WebApi.DTOs;
using WebApiTutorial250818.WebApi.Services;

namespace WebApiTutorial250818.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentReadDto>>> GetAll(CancellationToken ct)
            => Ok(await _service.GetAllAsync(ct));

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<StudentReadDto>> GetById(int id, CancellationToken ct)
        {
            var result = await _service.GetByIdAsync(id, ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<StudentReadDto>> Create(
            [FromBody] StudentCreateDto dto, CancellationToken ct, IValidator<StudentCreateDto> validator)
        {
            var validation = validator.Validate(dto);
            if (!validation.IsValid)
            {
                validation.Errors.ForEach(e => ModelState.AddModelError(e.PropertyName, e.ErrorMessage));
                return ValidationProblem(ModelState);
            }

            var id = await _service.CreateAsync(dto, ct);
            var created = await _service.GetByIdAsync(id, ct);
            return CreatedAtAction(nameof(GetById), new { id }, created);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(
            int id, [FromBody] StudentUpdateDto dto, CancellationToken ct, IValidator<StudentUpdateDto> validator)
        {
            var validation = validator.Validate(dto);
            if (!validation.IsValid)
            {
                validation.Errors.ForEach(e => ModelState.AddModelError(e.PropertyName, e.ErrorMessage));
                return ValidationProblem(ModelState);
            }
            var ok = await _service.UpdateAsync(id, dto, ct);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var ok = await _service.DeleteAsync(id, ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
