namespace WebApiTutorial250818.WebApi.Auth
{
    public record RegisterDto(string userName, string Email, string Password, string confirmPassword);
    public record LoginDto(string Email, string Password);
    public record AuthResponseDto(string AccessToken);
    public record ConnectStudentDto(int StudentId, string UserId);
}
