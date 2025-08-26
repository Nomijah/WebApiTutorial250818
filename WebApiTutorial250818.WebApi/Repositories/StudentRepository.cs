using Microsoft.EntityFrameworkCore;
using WebApiTutorial250818.WebApi.Data;
using WebApiTutorial250818.WebApi.Models;

namespace WebApiTutorial250818.WebApi.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext _ctx;

        public StudentRepository(SchoolContext ctx) => _ctx = ctx;

        public Task<List<Student>> GetAllAsync(CancellationToken ct = default) =>
            _ctx.Students.AsNoTracking().OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToListAsync(ct);

        public Task<Student?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _ctx.Students.FirstOrDefaultAsync(s => s.Id == id, ct);

        public Task<Student?> GetByUserIdAsync(string userId, CancellationToken ct = default)
        {
            return _ctx.Students.AsNoTracking().FirstOrDefaultAsync(s => s.UserId.ToString() == userId, ct);
        }

        public async Task AddAsync(Student student, CancellationToken ct = default)
        {
            await _ctx.Students.AddAsync(student, ct);
        }

        public Task UpdateAsync(Student student, CancellationToken ct = default)
        {
            _ctx.Students.Update(student);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Student student, CancellationToken ct = default)
        {
            _ctx.Students.Remove(student);
            return Task.CompletedTask;
        }

        public Task<bool> SaveChangesAsync(CancellationToken ct = default) =>
            _ctx.SaveChangesAsync(ct).ContinueWith(t => t.Result > 0, ct);
    }
}
