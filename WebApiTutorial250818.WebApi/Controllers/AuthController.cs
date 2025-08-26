using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApiTutorial250818.WebApi.Auth;
using WebApiTutorial250818.WebApi.DTOs;

namespace WebApiTutorial250818.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _users;
        private readonly IJwtTokenService _tokens;
        private readonly IAuthService _authService;

        public AuthController(UserManager<User> users, IJwtTokenService tokens, IAuthService authService)
        {
            _users = users;
            _tokens = tokens;
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = new User { UserName = dto.Email, Email = dto.Email };
            IdentityResult result = await _users.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
            }

            await _users.AddToRoleAsync(user, "user");
            return StatusCode(201);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _users.FindByEmailAsync(dto.Email);
            if (user is null)
            {
                return BadRequest(new { Errors = new[] { "Bad credentials." } });
            }

            var ok = await _users.CheckPasswordAsync(user, dto.Password);
            if (!ok)
            {
                return BadRequest(new { Errors = new[] { "Bad credentials" } });
            }

            var roles = await _users.GetRolesAsync(user);

            var token = _tokens.CreateToken(user, roles);
            return Ok(new AuthResponseDto(token));
        }

        [HttpPut]
        public async Task<IActionResult> ConnectStudent([FromBody] ConnectStudentDto dto)
        {
            var result = await _authService.ConnectStudent(dto.StudentId, dto.UserId);
            return result ? NoContent() : NotFound();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<StudentReadDto>> Me()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            // Get the user from the database
            var user = await _users.FindByIdAsync(userId);
            if (user is null)
            {
                return NotFound();
            }

            var result = await _authService.GetStudentData(userId);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
