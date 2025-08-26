using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiTutorial250818.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class teacherAndDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Students",
                newName: "PersonalEmail");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherDepartments",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherDepartments", x => new { x.TeacherId, x.DepartmentId });
                    table.ForeignKey(
                        name: "FK_TeacherDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherDepartments_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Department of Computer Science", "Computer Science" },
                    { 2, "Department of Mathematics", "Mathematics" }
                });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: null);

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "BirthDate", "FirstName", "LastName", "PersonalEmail", "UserId" },
                values: new object[,]
                {
                    { 1, new DateOnly(1938, 1, 10), "Donald", "Knuth", "donald.knuth@example.com", null },
                    { 2, new DateOnly(1939, 11, 7), "Barbara", "Liskov", "barbara.liskov@example.com", null }
                });

            migrationBuilder.InsertData(
                table: "TeacherDepartments",
                columns: new[] { "DepartmentId", "TeacherId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherDepartments_DepartmentId",
                table: "TeacherDepartments",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherDepartments");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "PersonalEmail",
                table: "Students",
                newName: "Email");
        }
    }
}
