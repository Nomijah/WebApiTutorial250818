using Microsoft.EntityFrameworkCore;
using WebApiTutorial250818.WebApi.Models;

namespace WebApiTutorial250818.WebApi.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<TeacherDepartment> TeacherDepartments => Set<TeacherDepartment>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
                e.Property(x => x.LastName).HasMaxLength(100).IsRequired();
                e.Property(x => x.PersonalEmail).HasMaxLength(255);
                e.Property(x => x.BirthDate);
            });

            modelBuilder.Entity<Course>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Title).HasMaxLength(200).IsRequired();
                e.Property(x => x.Credits).IsRequired();
            });

            modelBuilder.Entity<Department>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).HasMaxLength(100).IsRequired();
                e.Property(x => x.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<Teacher>(t =>
            {
                t.HasKey(x => x.Id);
                t.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
                t.Property(x => x.LastName).HasMaxLength(100).IsRequired();
                t.Property(x => x.PersonalEmail).HasMaxLength(255);
                t.Property(x => x.BirthDate);
            });

            modelBuilder.Entity<TeacherDepartment>(td =>
            {
                td.HasKey(t => new { t.TeacherId, t.DepartmentId });
                td.HasOne(t => t.Teacher).WithMany().HasForeignKey(t => t.TeacherId);
                td.HasOne(d => d.Department).WithMany().HasForeignKey(d => d.DepartmentId);
            });

            // Seed-data
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    FirstName = "Ada",
                    LastName = "Lovelace",
                    PersonalEmail = "ada.lovelace@example.com",
                    BirthDate = new DateOnly(1815, 12, 10)
                },
                new Student
                {
                    Id = 2,
                    FirstName = "Alan",
                    LastName = "Turing",
                    PersonalEmail = "alan.turing@example.com",
                    BirthDate = new DateOnly(1912, 6, 23)
                },
                new Student
                {
                    Id = 3,
                    FirstName = "Grace",
                    LastName = "Hopper",
                    PersonalEmail = "grace.hopper@example.com",
                    BirthDate = new DateOnly(1906, 12, 9)
                }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Title = "Introduction to Computer Science",
                    Credits = 7
                },
                new Course
                {
                    Id = 2,
                    Title = "Algorithms and Data Structures",
                    Credits = 6
                },
                new Course
                {
                    Id = 3,
                    Title = "Database Systems",
                    Credits = 5
                }
            );

            // Seed Departments
            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Name = "Computer Science",
                    Description = "Department of Computer Science"
                },
                new Department
                {
                    Id = 2,
                    Name = "Mathematics",
                    Description = "Department of Mathematics"
                }
            );

            // Seed Teachers
            modelBuilder.Entity<Teacher>().HasData(
                new Teacher
                {
                    Id = 1,
                    FirstName = "Donald",
                    LastName = "Knuth",
                    PersonalEmail = "donald.knuth@example.com",
                    BirthDate = new DateOnly(1938, 1, 10),
                    UserId = null
                },
                new Teacher
                {
                    Id = 2,
                    FirstName = "Barbara",
                    LastName = "Liskov",
                    PersonalEmail = "barbara.liskov@example.com",
                    BirthDate = new DateOnly(1939, 11, 7),
                    UserId = null
                }
            );

            modelBuilder.Entity<TeacherDepartment>().HasData(
                new TeacherDepartment
                {
                    TeacherId = 1,
                    DepartmentId = 1
                },
                new TeacherDepartment
                {
                    TeacherId = 2,
                    DepartmentId = 1
                },
                new TeacherDepartment
                {
                    TeacherId = 2,
                    DepartmentId = 2
                }
            );


        }
    }
}
