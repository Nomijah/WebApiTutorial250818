using Microsoft.EntityFrameworkCore;
using WebApiTutorial250818.WebApi.Data;
using WebApiTutorial250818.WebApi.Models;

namespace WebApiTutorial250818.WebApi.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SchoolContext _ctx;

        public TeacherRepository(SchoolContext ctx) => _ctx = ctx;

        public Task<List<Teacher>> GetAllAsync(CancellationToken ct = default) =>
            _ctx.Teachers.AsNoTracking().OrderBy(t => t.LastName).ThenBy(t => t.FirstName).ToListAsync(ct);

        public Task<Teacher?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _ctx.Teachers.FirstOrDefaultAsync(t => t.Id == id, ct);

        public async Task AddAsync(Teacher teacher, CancellationToken ct = default)
        {
            await _ctx.Teachers.AddAsync(teacher, ct);
        }

        public Task UpdateAsync(Teacher teacher, CancellationToken ct = default)
        {
            _ctx.Teachers.Update(teacher);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Teacher teacher, CancellationToken ct = default)
        {
            _ctx.Teachers.Remove(teacher);
            return Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken ct = default) =>
            await _ctx.SaveChangesAsync(ct) > 0;
    }
}
