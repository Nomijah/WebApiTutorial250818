
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApiTutorial250818.WebApi.Data;
using WebApiTutorial250818.WebApi.Repositories;
using WebApiTutorial250818.WebApi.Services;

namespace WebApiTutorial250818.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            builder.Services.AddDbContext<SchoolContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseService, CourseService>();

            builder.Services.AddAutoMapper(typeof(Program)); // AutoMapper för DTOs

            builder.Services.AddControllers()
                .AddNewtonsoftJson(); // Patchdoc med Newtonsoft.Json

            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>(); // Registrera validators

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo { Title = "School API", Version = "v1" });
            });

            builder.Services.AddSwaggerGenNewtonsoftSupport(); // Patchdoc med Newtonsoft.Json

            var app = builder.Build();

            // Skapa DB och kör ev. pending migrations vid start
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SchoolContext>();
                db.Database.Migrate(); // skapar db + kör migrations
            }

            if (app.Environment.IsDevelopment())
            {
                // Fixed problem with Swagger not being able to read the OpenAPI version
                app.UseSwagger(c =>
                {
                    c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
                });
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "School API v1");
                });
            }

            app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}
