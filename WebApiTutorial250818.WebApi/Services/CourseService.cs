using AutoMapper;
using WebApiTutorial250818.WebApi.DTOs;
using WebApiTutorial250818.WebApi.Models;
using WebApiTutorial250818.WebApi.Repositories;

namespace WebApiTutorial250818.WebApi.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<CourseReadDto>> GetAllAsync(CancellationToken ct = default)
        {
            var courses = await _repo.GetAllAsync(ct);
            return _mapper.Map<List<CourseReadDto>>(courses);
        }

        public async Task<CourseReadDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var course = await _repo.GetByIdAsync(id, ct);
            return course == null ? null : _mapper.Map<CourseReadDto>(course);
        }

        public async Task<int> CreateAsync(CourseCreateDto dto, CancellationToken ct = default)
        {
            var course = _mapper.Map<Course>(dto);
            await _repo.AddAsync(course, ct);
            var saved = await _repo.SaveChangesAsync(ct);
            return saved ? course.Id : 0;
        }

        public async Task<bool> UpdateAsync(int id, CourseUpdateDto dto, CancellationToken ct = default)
        {
            var course = await _repo.GetByIdAsync(id, ct);
            if (course == null)
            {
                return false;
            }
            _mapper.Map(dto, course);
            await _repo.UpdateAsync(course, ct);
            return await _repo.SaveChangesAsync(ct);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var course = await _repo.GetByIdAsync(id, ct);
            if (course == null)
            {
                return false;
            }
            await _repo.DeleteAsync(course, ct);
            return await _repo.SaveChangesAsync(ct);
        }
    }
}
