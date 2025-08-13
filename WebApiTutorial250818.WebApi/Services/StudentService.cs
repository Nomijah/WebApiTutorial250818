using Microsoft.IdentityModel.Tokens;
using WebApiTutorial250818.WebApi.DTOs;
using WebApiTutorial250818.WebApi.Models;
using WebApiTutorial250818.WebApi.Repositories;

namespace WebApiTutorial250818.WebApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo) => _repo = repo;

        public async Task<List<StudentReadDto>> GetAllAsync(CancellationToken ct = default)
        {
            var students = await _repo.GetAllAsync(ct);
            return students.Select(ToReadDto).OrderBy(s => s.Id).ToList();
        }

        public async Task<StudentReadDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var s = await _repo.GetByIdAsync(id, ct);
            return s is null ? null : ToReadDto(s);
        }

        public async Task<int> CreateAsync(StudentCreateDto dto, CancellationToken ct = default)
        {
            var s = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                BirthDate = dto.BirthDate
            };

            await _repo.AddAsync(s, ct);
            await _repo.SaveChangesAsync(ct);
            return s.Id;
        }

        public async Task<bool> UpdateAsync(int id, StudentUpdateDto dto, CancellationToken ct = default)
        {
            var s = await _repo.GetByIdAsync(id, ct);
            if (s is null) return false;

            s.FirstName = dto.FirstName;
            s.LastName = dto.LastName;
            s.Email = dto.Email;
            s.BirthDate = dto.BirthDate;

            await _repo.UpdateAsync(s, ct);
            return await _repo.SaveChangesAsync(ct);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var s = await _repo.GetByIdAsync(id, ct);
            if (s is null) return false;

            await _repo.DeleteAsync(s, ct);
            return await _repo.SaveChangesAsync(ct);
        }

        private static StudentReadDto ToReadDto(Student s) => new()
        {
            Id = s.Id,
            FullName = $"{s.FirstName} {s.LastName}",
            Email = s.Email,
            BirthDate = s.BirthDate
        };
    }
}
