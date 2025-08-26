using WebApiTutorial250818.WebApi.Models;

namespace WebApiTutorial250818.WebApi.Repositories
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetAllAsync(CancellationToken ct = default);
        Task<Teacher?> GetByIdAsync(int id, CancellationToken ct = default);
        Task AddAsync(Teacher teacher, CancellationToken ct = default);
        Task UpdateAsync(Teacher teacher, CancellationToken ct = default);
        Task DeleteAsync(Teacher teacher, CancellationToken ct = default);
        Task<bool> SaveChangesAsync(CancellationToken ct = default);
    }
}
