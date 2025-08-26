using WebApiTutorial250818.WebApi.DTOs;
using WebApiTutorial250818.WebApi.Models;

namespace WebApiTutorial250818.WebApi.Services
{
    public interface IStudentService
    {
        Task<List<StudentReadDto>> GetAllAsync(CancellationToken ct = default);
        Task<StudentReadDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(StudentCreateDto dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(int id, StudentUpdateDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
