using AutoMapper;
using WebApiTutorial250818.WebApi.DTOs;
using WebApiTutorial250818.WebApi.Models;

namespace WebApiTutorial250818.WebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Entity to DTO
            CreateMap<Student, StudentReadDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<Course, CourseReadDto>();

            // DTO to Entity
            CreateMap<StudentCreateDto, Student>();
            CreateMap<StudentUpdateDto, Student>();
            CreateMap<CourseCreateDto, Course>();
            CreateMap<CourseUpdateDto, Course>();

            // PatchDocument to Entity
            CreateMap<StudentPatchDto, Student>().ReverseMap();
        }
    }
}
