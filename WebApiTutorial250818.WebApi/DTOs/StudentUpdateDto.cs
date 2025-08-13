namespace WebApiTutorial250818.WebApi.DTOs
{
    public class StudentUpdateDto
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string? Email { get; set; }
        public DateOnly? BirthDate { get; set; }
    }
}
