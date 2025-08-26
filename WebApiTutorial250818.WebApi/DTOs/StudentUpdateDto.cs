namespace WebApiTutorial250818.WebApi.DTOs
{
    public class StudentUpdateDto
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string? PersonalEmail { get; set; }
        public DateOnly? BirthDate { get; set; }
    }
}
