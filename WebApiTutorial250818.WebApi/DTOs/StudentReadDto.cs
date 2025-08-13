namespace WebApiTutorial250818.WebApi.DTOs
{
    public class StudentReadDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string? Email { get; set; }
        public DateOnly? BirthDate { get; set; }
    }
}
