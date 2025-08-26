namespace WebApiTutorial250818.WebApi.Models
{
    public class TeacherDepartment
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

    }
}
