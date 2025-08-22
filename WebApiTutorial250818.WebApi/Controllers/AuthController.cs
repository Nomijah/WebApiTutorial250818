using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiTutorial250818.WebApi.Auth;

namespace WebApiTutorial250818.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _users;
        private readonly IJwtTokenService _tokens;

        public AuthController(UserManager<IdentityUser> users, IJwtTokenService tokens)
        {
            _users = users;
            _tokens = tokens;
        }

        [HttpPost("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = new IdentityUser { UserName = dto.Email, Email = dto.Email };
            var result = await _users.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });

            return StatusCode(201);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _users.FindByEmailAsync(dto.Email);
            if (user is null) return BadRequest(new { Errors = new[] { "Invalid credentials" } });

            var ok = await _users.CheckPasswordAsync(user, dto.Password);
            if (!ok) return BadRequest(new { Errors = new[] { "Invalid credentials" } });

            var token = _tokens.CreateToken(user);
            return Ok(new AuthResponseDto(token));
        }
    }
}
