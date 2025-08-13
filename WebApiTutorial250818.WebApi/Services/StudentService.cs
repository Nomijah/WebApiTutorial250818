using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using WebApiTutorial250818.WebApi.DTOs;
using WebApiTutorial250818.WebApi.Models;
using WebApiTutorial250818.WebApi.Repositories;

namespace WebApiTutorial250818.WebApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<StudentReadDto>> GetAllAsync(CancellationToken ct = default)
        {
            var students = await _repo.GetAllAsync(ct);
            return _mapper.Map<List<StudentReadDto>>(students);
        }

        public async Task<StudentReadDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var s = await _repo.GetByIdAsync(id, ct);
            return _mapper.Map<StudentReadDto?>(s);
        }

        public async Task<int> CreateAsync(StudentCreateDto dto, CancellationToken ct = default)
        {
            var s = _mapper.Map<Student>(dto);

            await _repo.AddAsync(s, ct);
            await _repo.SaveChangesAsync(ct);
            return s.Id;
        }

        public async Task<bool> UpdateAsync(int id, StudentUpdateDto dto, CancellationToken ct = default)
        {
            var s = await _repo.GetByIdAsync(id, ct);
            if (s is null) return false;

            _mapper.Map(dto, s); // uppdaterar bara de mappade egenskaperna

            await _repo.UpdateAsync(s, ct);
            return await _repo.SaveChangesAsync(ct);
        }

        public async Task<bool> PatchAsync(int id, JsonPatchDocument<StudentPatchDto> patchDoc, CancellationToken ct = default)
        {
            var student = await _repo.GetByIdAsync(id, ct);
            if (student is null) return false;

            // Skapa en DTO för patchning
            var studentPatchDto = _mapper.Map<StudentPatchDto>(student);

            patchDoc.ApplyTo(studentPatchDto); // modifierar bara det som klienten skickar in

            _mapper.Map(studentPatchDto, student); // uppdaterar student med patchade värden

            await _repo.UpdateAsync(student, ct);
            return await _repo.SaveChangesAsync(ct);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var s = await _repo.GetByIdAsync(id, ct);
            if (s is null) return false;

            await _repo.DeleteAsync(s, ct);
            return await _repo.SaveChangesAsync(ct);
        }
    }
}
