using AutoMapper;
using WebApiTutorial250818.WebApi.DTOs;
using WebApiTutorial250818.WebApi.Models;
using WebApiTutorial250818.WebApi.Repositories;

namespace WebApiTutorial250818.WebApi.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<DepartmentReadDto>> GetAllAsync(CancellationToken ct = default)
        {
            var departments = await _repo.GetAllAsync(ct);
            return _mapper.Map<List<DepartmentReadDto>>(departments);
        }

        public async Task<DepartmentReadDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var department = await _repo.GetByIdAsync(id, ct);
            return department == null ? null : _mapper.Map<DepartmentReadDto>(department);
        }

        public async Task<int> CreateAsync(DepartmentCreateDto dto, CancellationToken ct = default)
        {
            var department = _mapper.Map<Department>(dto);
            await _repo.AddAsync(department, ct);
            var saved = await _repo.SaveChangesAsync(ct);
            return saved ? department.Id : 0;
        }

        public async Task<bool> UpdateAsync(int id, DepartmentUpdateDto dto, CancellationToken ct = default)
        {
            var department = await _repo.GetByIdAsync(id, ct);
            if (department == null) return false;
            _mapper.Map(dto, department);
            await _repo.UpdateAsync(department, ct);
            return await _repo.SaveChangesAsync(ct);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var department = await _repo.GetByIdAsync(id, ct);
            if (department == null) return false;
            await _repo.DeleteAsync(department, ct);
            return await _repo.SaveChangesAsync(ct);
        }
    }
}
