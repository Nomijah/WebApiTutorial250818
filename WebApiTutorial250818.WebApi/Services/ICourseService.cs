using WebApiTutorial250818.WebApi.Common;
using WebApiTutorial250818.WebApi.DTOs;
using WebApiTutorial250818.WebApi.Models;

namespace WebApiTutorial250818.WebApi.Services
{
    public interface ICourseService
    {
        Task<List<CourseReadDto>> GetAllAsync(CancellationToken ct = default);
        Task<Result<CourseReadDto?>> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Result<int>> CreateAsync(CourseCreateDto dto, CancellationToken ct = default);
        Task<Result> UpdateAsync(int id, CourseUpdateDto dto, CancellationToken ct = default);
        Task<Result> DeleteAsync(int id, CancellationToken ct = default);
    }
}
