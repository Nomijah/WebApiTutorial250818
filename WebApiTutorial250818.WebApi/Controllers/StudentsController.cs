using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApiTutorial250818.WebApi.DTOs;
using WebApiTutorial250818.WebApi.Models;
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
        public async Task<ActionResult<StudentReadDto>> GetById(int id, CancellationToken ct)
        {
            var result = await _service.GetByIdAsync(id, ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<StudentReadDto>> Create([FromBody] StudentCreateDto dto, CancellationToken ct)
        {
            var id = await _service.CreateAsync(dto, ct);
            var created = await _service.GetByIdAsync(id, ct);
            return CreatedAtAction(nameof(GetById), new { id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentUpdateDto dto, CancellationToken ct)
        {
            var ok = await _service.UpdateAsync(id, dto, ct);
            return ok ? NoContent() : NotFound();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id,
                [FromBody] JsonPatchDocument<StudentPatchDto> patchDoc,
                CancellationToken ct)
        {
            if (patchDoc is null)
                return BadRequest();

            var result = await _service.PatchAsync(id, patchDoc, ct);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var ok = await _service.DeleteAsync(id, ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
