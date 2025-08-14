using WebApiTutorial250818.WebApi.DTOs;
using WebApiTutorial250818.WebApi.Models;

namespace WebApiTutorial250818.WebApi.Services
{
    public interface ICourseService
    {
        Task<List<CourseReadDto>> GetAllAsync(CancellationToken ct = default);
        Task<CourseReadDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(CourseCreateDto dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(int id, CourseUpdateDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
