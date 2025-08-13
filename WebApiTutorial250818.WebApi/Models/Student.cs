namespace WebApiTutorial250818.WebApi.Models
{
    public class Student
    {
        public int Id { get; set; }                 // PK
        public string FirstName { get; set; } = ""; // obligatoriskt via modelBuilder
        public string LastName { get; set; } = "";
        public string? Email { get; set; }
        public DateOnly? BirthDate { get; set; }    // valfritt fält
    }
}
