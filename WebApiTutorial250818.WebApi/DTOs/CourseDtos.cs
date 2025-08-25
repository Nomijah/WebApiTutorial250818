namespace WebApiTutorial250818.WebApi.DTOs
{
    public record CourseReadDto(int Id, string Title, int Credits);
    public record CourseCreateDto(string Title, int Credits);
    public record CourseUpdateDto(string Title, int Credits);
}
