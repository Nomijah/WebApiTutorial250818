
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApiTutorial250818.WebApi.Auth;
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
            builder.Services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

            builder.Services.AddAutoMapper(typeof(Program)); // AutoMapper för DTOs

            builder.Services.AddControllers()
                .AddNewtonsoftJson(); // Patchdoc med Newtonsoft.Json

            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>(); // Registrera validators

            // Identity
            builder.Services
                .AddIdentityCore<IdentityUser>(o =>
                {
                    o.User.RequireUniqueEmail = true; // Kräver unika e-postadresser
                    o.Password.RequiredLength = 8; // Minsta längd för lösenord
                })
                .AddEntityFrameworkStores<AuthDbContext>() // Använd EF Core för Identity
                .AddDefaultTokenProviders(); // Lägg till standard token providers för Identity

            // JWT
            var jwt = builder.Configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = jwt["Issuer"],
                        ValidAudience = jwt["Audience"],
                        IssuerSigningKey = key,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo { Title = "School API", Version = "v1" });
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme // Definiera säkerhetsdefinitionen för JWT
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Ange 'Bearer {token}'"
                });
                o.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            builder.Services.AddSwaggerGenNewtonsoftSupport();


            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Skapa DB och kör ev. pending migrations vid start (utbildningssyfte)
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SchoolContext>();
                db.Database.Migrate(); // skapar db + kör migrations
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(c =>
                {
                    c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
                });
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "School API v1");
                });
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}
