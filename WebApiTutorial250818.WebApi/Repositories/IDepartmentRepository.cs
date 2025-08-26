using WebApiTutorial250818.WebApi.Models;

namespace WebApiTutorial250818.WebApi.Repositories
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync(CancellationToken ct = default);
        Task<Department?> GetByIdAsync(int id, CancellationToken ct = default);
        Task AddAsync(Department department, CancellationToken ct = default);
        Task UpdateAsync(Department department, CancellationToken ct = default);
        Task DeleteAsync(Department department, CancellationToken ct = default);
        Task<bool> SaveChangesAsync(CancellationToken ct = default);
    }
}
