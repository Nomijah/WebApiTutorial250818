namespace WebApiTutorial250818.WebApi.Models
{
    public class Teacher
    {
        public int Id { get; set; }                 
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string? PersonalEmail { get; set; } = "";
        public DateOnly? BirthDate { get; set; }
        public Guid? UserId { get; set; }

    }
}
