using Microsoft.EntityFrameworkCore;
using WebApiTutorial250818.WebApi.Data;
using WebApiTutorial250818.WebApi.Models;

namespace WebApiTutorial250818.WebApi.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SchoolContext _ctx;

        public CourseRepository(SchoolContext ctx) => _ctx = ctx;

        public Task<List<Course>> GetAllAsync(CancellationToken ct = default) =>
            _ctx.Courses.AsNoTracking().OrderBy(c => c.Id).ToListAsync(ct);

        public Task<Course?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _ctx.Courses.FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task AddAsync(Course course, CancellationToken ct = default)
        {
            await _ctx.Courses.AddAsync(course, ct);
        }

        public Task UpdateAsync(Course course, CancellationToken ct = default)
        {
            _ctx.Courses.Update(course);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Course course, CancellationToken ct = default)
        {
            _ctx.Courses.Remove(course);
            return Task.CompletedTask;
        }

        public Task<bool> SaveChangesAsync(CancellationToken ct = default) =>
            _ctx.SaveChangesAsync(ct).ContinueWith(t => t.Result > 0, ct);
    }
}
