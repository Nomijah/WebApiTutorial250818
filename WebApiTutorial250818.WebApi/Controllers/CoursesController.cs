using Microsoft.AspNetCore.Mvc;
using WebApiTutorial250818.WebApi.Common;
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
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var result = await _service.GetByIdAsync(id, ct);
            return result.ToActionResult(this, value => Ok(value));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CourseReadDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Create([FromBody] CourseCreateDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var result = await _service.CreateAsync(dto, ct);
            return result.ToActionResult(this, id => CreatedAtAction(nameof(GetById), new { id }, null));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(int id, [FromBody] CourseUpdateDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            var result = await _service.UpdateAsync(id, dto, ct);
            return result.ToActionResult(this);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var result = await _service.DeleteAsync(id, ct);
            return result.ToActionResult(this);
        }
    }
}
