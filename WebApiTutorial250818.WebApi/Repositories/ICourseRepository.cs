using WebApiTutorial250818.WebApi.Models;

namespace WebApiTutorial250818.WebApi.Repositories
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllAsync(CancellationToken ct = default);
        Task<Course?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Course?> GetByTitleAsync(string title, CancellationToken ct = default);
        Task AddAsync(Course course, CancellationToken ct = default);
        Task UpdateAsync(Course course, CancellationToken ct = default);
        Task DeleteAsync(Course course, CancellationToken ct = default);
        Task<bool> SaveChangesAsync(CancellationToken ct = default);
    }
}
