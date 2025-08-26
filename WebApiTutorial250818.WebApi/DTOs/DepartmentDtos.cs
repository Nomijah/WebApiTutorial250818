namespace WebApiTutorial250818.WebApi.DTOs
{
    public record DepartmentReadDto(
        int Id,
        string Name,
        string? Description
    );

    public record DepartmentCreateDto(
        string Name,
        string? Description
    );

    public record DepartmentUpdateDto(
        string Name,
        string? Description
    );
}
