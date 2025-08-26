using WebApiTutorial250818.WebApi.DTOs;

namespace WebApiTutorial250818.WebApi.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentReadDto>> GetAllAsync(CancellationToken ct = default);
        Task<DepartmentReadDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(DepartmentCreateDto dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(int id, DepartmentUpdateDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
