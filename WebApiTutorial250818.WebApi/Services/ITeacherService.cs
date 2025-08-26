using WebApiTutorial250818.WebApi.DTOs;

namespace WebApiTutorial250818.WebApi.Services
{
    public interface ITeacherService
    {
        Task<List<TeacherReadDto>> GetAllAsync(CancellationToken ct = default);
        Task<TeacherReadDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(TeacherCreateDto dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(int id, TeacherUpdateDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
