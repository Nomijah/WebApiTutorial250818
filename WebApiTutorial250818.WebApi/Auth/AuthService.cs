using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebApiTutorial250818.WebApi.DTOs;
using WebApiTutorial250818.WebApi.Repositories;

namespace WebApiTutorial250818.WebApi.Auth
{
    public interface IAuthService
    {
        Task<bool> ConnectStudent(int studentId, string userId);
        Task<StudentReadDto> GetStudentData(string userId);
    }
    public class AuthService : IAuthService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly UserManager<User> _userManager;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public AuthService(IStudentRepository studentRepo, 
            UserManager<User> userManager, IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepo = studentRepo;
            _userManager = userManager;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<bool> ConnectStudent(int studentId, string userId)
        {
            var student = await _studentRepo.GetByIdAsync(studentId);
            if (student is null)
            {
                return false;
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return false;
            }
            student.UserId = user.Id;
            await _studentRepo.UpdateAsync(student);
            return await _studentRepo.SaveChangesAsync();
        }

        public async Task<StudentReadDto?> GetStudentData(string userId)
        {
            var result = await _studentRepository.GetByUserIdAsync(userId);
            return result is null ? null : _mapper.Map<StudentReadDto>(result);

        }
    }
}
