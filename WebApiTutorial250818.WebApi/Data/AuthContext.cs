using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApiTutorial250818.WebApi.Data
{
    public class AuthContext : IdentityDbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed roles
            var adminRole = new IdentityRole("Admin") { Id = "1", NormalizedName = "ADMIN", ConcurrencyStamp = "STATIC-ADMIN-ROLE-CONCURRENCYSTAMP" };
            var userRole = new IdentityRole("User") { Id = "2", NormalizedName = "USER", ConcurrencyStamp = "STATIC-USER-ROLE-CONCURRENCYSTAMP" };
            modelBuilder.Entity<IdentityRole>().HasData(adminRole, userRole);

            // Seed users
            var adminUser = new IdentityUser
            {
                Id = "a1",
                UserName = "admin@school.local",
                NormalizedUserName = "ADMIN@SCHOOL.LOCAL",
                Email = "admin@school.local",
                NormalizedEmail = "ADMIN@SCHOOL.LOCAL",
                EmailConfirmed = true,
                SecurityStamp = "STATIC-ADMIN-SECURITYSTAMP",
                ConcurrencyStamp = "STATIC-ADMIN-ROLE-CONCURRENCYSTAMP",
                PasswordHash = "AQAAAAIAAYagAAAAEHfdPaPu3AoXt73wEtI9kk74dORAiPsgVJVbKJDfU6UNi2wjuO11LCYGHrCwUxlthQ=="
            };

            var regularUser = new IdentityUser
            {
                Id = "u1",
                UserName = "user@school.local",
                NormalizedUserName = "USER@SCHOOL.LOCAL",
                Email = "user@school.local",
                NormalizedEmail = "USER@SCHOOL.LOCAL",
                EmailConfirmed = true,
                SecurityStamp = "STATIC-USER-SECURITYSTAMP",
                ConcurrencyStamp = "STATIC-USER-ROLE-CONCURRENCYSTAMP",
                PasswordHash = "AQAAAAIAAYagAAAAEJHOQD8WJ9FhT7jFt5WPjdw+iA6FmLgQSsWA+9ranctpdC3Xy2v4vtign4B+sADe+g=="
            };

            modelBuilder.Entity<IdentityUser>().HasData(adminUser, regularUser);

            // Assign roles to users
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "a1", RoleId = "1" }, // admin
                new IdentityUserRole<string> { UserId = "u1", RoleId = "2" }  // regular
            );
        }
    }
}
