using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiTutorial250818.WebApi.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class addedSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "STATIC-ADMIN-ROLE-CONCURRENCYSTAMP", "Admin", "ADMIN" },
                    { "2", "STATIC-USER-ROLE-CONCURRENCYSTAMP", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a1", 0, "STATIC-ADMIN-ROLE-CONCURRENCYSTAMP", "admin@school.local", true, false, null, "ADMIN@SCHOOL.LOCAL", "ADMIN@SCHOOL.LOCAL", "AQAAAAIAAYagAAAAEHfdPaPu3AoXt73wEtI9kk74dORAiPsgVJVbKJDfU6UNi2wjuO11LCYGHrCwUxlthQ==", null, false, "STATIC-ADMIN-SECURITYSTAMP", false, "admin@school.local" },
                    { "u1", 0, "STATIC-USER-ROLE-CONCURRENCYSTAMP", "user@school.local", true, false, null, "USER@SCHOOL.LOCAL", "USER@SCHOOL.LOCAL", "AQAAAAIAAYagAAAAEJHOQD8WJ9FhT7jFt5WPjdw+iA6FmLgQSsWA+9ranctpdC3Xy2v4vtign4B+sADe+g==", null, false, "STATIC-USER-SECURITYSTAMP", false, "user@school.local" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "a1" },
                    { "2", "u1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "a1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "u1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "u1");
        }
    }
}
