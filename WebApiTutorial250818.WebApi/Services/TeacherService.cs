using AutoMapper;
using WebApiTutorial250818.WebApi.DTOs;
using WebApiTutorial250818.WebApi.Models;
using WebApiTutorial250818.WebApi.Repositories;

namespace WebApiTutorial250818.WebApi.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _repo;
        private readonly IMapper _mapper;

        public TeacherService(ITeacherRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<TeacherReadDto>> GetAllAsync(CancellationToken ct = default)
        {
            var teachers = await _repo.GetAllAsync(ct);
            return _mapper.Map<List<TeacherReadDto>>(teachers);
        }

        public async Task<TeacherReadDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var teacher = await _repo.GetByIdAsync(id, ct);
            return teacher == null ? null : _mapper.Map<TeacherReadDto>(teacher);
        }

        public async Task<int> CreateAsync(TeacherCreateDto dto, CancellationToken ct = default)
        {
            var teacher = _mapper.Map<Teacher>(dto);
            await _repo.AddAsync(teacher, ct);
            var saved = await _repo.SaveChangesAsync(ct);
            return saved ? teacher.Id : 0;
        }

        public async Task<bool> UpdateAsync(int id, TeacherUpdateDto dto, CancellationToken ct = default)
        {
            var teacher = await _repo.GetByIdAsync(id, ct);
            if (teacher == null) return false;
            _mapper.Map(dto, teacher);
            await _repo.UpdateAsync(teacher, ct);
            return await _repo.SaveChangesAsync(ct);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var teacher = await _repo.GetByIdAsync(id, ct);
            if (teacher == null) return false;
            await _repo.DeleteAsync(teacher, ct);
            return await _repo.SaveChangesAsync(ct);
        }
    }
}
