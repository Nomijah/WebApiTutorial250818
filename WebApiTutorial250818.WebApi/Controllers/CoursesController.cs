using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApiTutorial250818.WebApi.DTOs;
using WebApiTutorial250818.WebApi.Services;

namespace WebApiTutorial250818.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service) => _service = service;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CourseReadDto>), 200)]
        public async Task<ActionResult<IEnumerable<CourseReadDto>>> GetAll(CancellationToken ct)
            => Ok(await _service.GetAllAsync(ct));

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CourseReadDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CourseReadDto>> GetById(int id, CancellationToken ct)
        {
            var result = await _service.GetByIdAsync(id, ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CourseReadDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CourseReadDto>> Create(
            [FromBody] CourseCreateDto dto, CancellationToken ct, IValidator<CourseCreateDto> validator)
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
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(
            int id, [FromBody] CourseUpdateDto dto, CancellationToken ct, IValidator<CourseUpdateDto> validator)
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
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var ok = await _service.DeleteAsync(id, ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
