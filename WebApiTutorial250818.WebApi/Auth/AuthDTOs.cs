namespace WebApiTutorial250818.WebApi.Auth
{
    public record RegisterDto(string Email, string Password);
    public record LoginDto(string Email, string Password);
    public record AuthResponseDto(string AccessToken);
}
