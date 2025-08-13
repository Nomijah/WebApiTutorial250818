using Microsoft.EntityFrameworkCore;
using WebApiTutorial250818.WebApi.Models;

namespace WebApiTutorial250818.WebApi.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        public DbSet<Student> Students => Set<Student>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
                e.Property(x => x.LastName).HasMaxLength(100).IsRequired();
                e.Property(x => x.Email).HasMaxLength(255);
                e.Property(x => x.BirthDate);
            });

            // Seed-data
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    FirstName = "Ada",
                    LastName = "Lovelace",
                    Email = "ada.lovelace@example.com",
                    BirthDate = new DateOnly(1815, 12, 10)
                },
                new Student
                {
                    Id = 2,
                    FirstName = "Alan",
                    LastName = "Turing",
                    Email = "alan.turing@example.com",
                    BirthDate = new DateOnly(1912, 6, 23)
                },
                new Student
                {
                    Id = 3,
                    FirstName = "Grace",
                    LastName = "Hopper",
                    Email = "grace.hopper@example.com",
                    BirthDate = new DateOnly(1906, 12, 9)
                }
            );
        }
    }
}
