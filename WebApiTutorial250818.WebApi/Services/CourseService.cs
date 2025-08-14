using WebApiTutorial250818.WebApi.DTOs;
using WebApiTutorial250818.WebApi.Models;
using WebApiTutorial250818.WebApi.Repositories;
using AutoMapper;
using WebApiTutorial250818.WebApi.Common;

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

        public async Task<Result<CourseReadDto?>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var course = await _repo.GetByIdAsync(id, ct);
            if (course == null)
            {
                return Result<CourseReadDto?>.Fail(
                    new Error(ErrorType.NotFound, "course.not_found", $"Course {id} not found"));
            }
            return Result<CourseReadDto>.Success(_mapper.Map<CourseReadDto>(course));
        }

        public async Task<Result<int>> CreateAsync(CourseCreateDto dto, CancellationToken ct = default)
        {
            var titleExists = await _repo.GetByTitleAsync(dto.Title, ct);
            if (titleExists != null)
            {
                return Result<int>.Fail(
                    new Error(ErrorType.Conflict, "course.title_exists", $"Course with title '{dto.Title}' already exists"));
            }

            var course = _mapper.Map<Course>(dto);
            await _repo.AddAsync(course, ct);
            var saved = await _repo.SaveChangesAsync(ct);

            if (!saved)
            {
                return Result<int>.Fail(
                    new Error(ErrorType.Unexpected, "course.save_failed", "Failed to save the course"));
            }

            return Result<int>.Success(course.Id);
        }

        public async Task<Result> UpdateAsync(int id, CourseUpdateDto dto, CancellationToken ct = default)
        {
            var course = await _repo.GetByIdAsync(id, ct);
            if (course == null)
            {
                return Result<bool>.Fail(
                    new Error(ErrorType.NotFound, "course.not_found", $"Course {id} not found"));
            }
            _mapper.Map(dto, course);
            await _repo.UpdateAsync(course, ct);
            var saved = await _repo.SaveChangesAsync(ct);

            if (!saved)
            {
                return Result<int>.Fail(
                    new Error(ErrorType.Unexpected, "course.update_failed", "Failed to update the course"));
            }

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
        {
            var course = await _repo.GetByIdAsync(id, ct);
            if (course == null)
            {
                return Result<bool>.Fail(
                    new Error(ErrorType.NotFound, "course.not_found", $"Course {id} not found"));
            }
            await _repo.DeleteAsync(course, ct);
            var saved = await _repo.SaveChangesAsync(ct);

            if (!saved)
            {
                return Result<int>.Fail(
                    new Error(ErrorType.Unexpected, "course.delete_failed", "Failed to delete the course"));
            }

            return Result.Success();
        }
    }
}
