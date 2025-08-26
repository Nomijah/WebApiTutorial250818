using Microsoft.EntityFrameworkCore;
using WebApiTutorial250818.WebApi.Data;
using WebApiTutorial250818.WebApi.Models;

namespace WebApiTutorial250818.WebApi.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly SchoolContext _ctx;

        public DepartmentRepository(SchoolContext ctx) => _ctx = ctx;

        public Task<List<Department>> GetAllAsync(CancellationToken ct = default) =>
            _ctx.Departments.AsNoTracking().OrderBy(d => d.Name).ToListAsync(ct);

        public Task<Department?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _ctx.Departments.FirstOrDefaultAsync(d => d.Id == id, ct);

        public async Task AddAsync(Department department, CancellationToken ct = default)
        {
            await _ctx.Departments.AddAsync(department, ct);
        }

        public Task UpdateAsync(Department department, CancellationToken ct = default)
        {
            _ctx.Departments.Update(department);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Department department, CancellationToken ct = default)
        {
            _ctx.Departments.Remove(department);
            return Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken ct = default) =>
            await _ctx.SaveChangesAsync(ct) > 0;
    }
}
