namespace WebApiTutorial250818.WebApi.DTOs
{
    public record TeacherReadDto(
        int Id,
        string FullName,
        string? PersonalEmail,
        DateOnly? BirthDate
    );

    public record TeacherCreateDto(
        string FirstName,
        string LastName,
        string? PersonalEmail,
        DateOnly? BirthDate
    );

    public record TeacherUpdateDto(
        string FirstName,
        string LastName,
        string? PersonalEmail,
        DateOnly? BirthDate
    );
}
