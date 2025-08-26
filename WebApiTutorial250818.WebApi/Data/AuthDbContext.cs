using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiTutorial250818.WebApi.Auth;

namespace WebApiTutorial250818.WebApi.Data
{
    public class AuthDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed roles
            var adminRole = new IdentityRole<Guid>("Admin") { Id = new Guid("33333333-3333-3333-3333-333333333333"), NormalizedName = "ADMIN", ConcurrencyStamp = "STATIC-ADMIN-ROLE-CONCURRENCYSTAMP" };
            var userRole = new IdentityRole<Guid>("User") { Id = new Guid("44444444-4444-4444-4444-444444444444"), NormalizedName = "USER", ConcurrencyStamp = "STATIC-USER-ROLE-CONCURRENCYSTAMP" };
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(adminRole, userRole);

            // Seed users
            var adminUser = new User
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                UserName = "admin@school.local",
                NormalizedUserName = "ADMIN@SCHOOL.LOCAL",
                Email = "admin@school.local",
                NormalizedEmail = "ADMIN@SCHOOL.LOCAL",
                EmailConfirmed = true,
                SecurityStamp = "STATIC-ADMIN-SECURITYSTAMP",
                ConcurrencyStamp = "STATIC-ADMIN-ROLE-CONCURRENCYSTAMP",
                PasswordHash = "AQAAAAIAAYagAAAAEHfdPaPu3AoXt73wEtI9kk74dORAiPsgVJVbKJDfU6UNi2wjuO11LCYGHrCwUxlthQ==" // Admin123!
            };

            var regularUser = new User
            {
                Id = new Guid("22222222-2222-2222-2222-222222222222"),
                UserName = "user@school.local",
                NormalizedUserName = "USER@SCHOOL.LOCAL",
                Email = "user@school.local",
                NormalizedEmail = "USER@SCHOOL.LOCAL",
                EmailConfirmed = true,
                SecurityStamp = "STATIC-USER-SECURITYSTAMP",
                ConcurrencyStamp = "STATIC-USER-ROLE-CONCURRENCYSTAMP",
                PasswordHash = "AQAAAAIAAYagAAAAEJHOQD8WJ9FhT7jFt5WPjdw+iA6FmLgQSsWA+9ranctpdC3Xy2v4vtign4B+sADe+g==" // User123!
            };

            modelBuilder.Entity<User>().HasData(adminUser, regularUser);

            // Assign roles to users
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid> { UserId = new Guid("11111111-1111-1111-1111-111111111111"), RoleId = new Guid("33333333-3333-3333-3333-333333333333") }, // admin
                new IdentityUserRole<Guid> { UserId = new Guid("22222222-2222-2222-2222-222222222222"), RoleId = new Guid("44444444-4444-4444-4444-444444444444") }  // regular
            );
        }
    }
}
