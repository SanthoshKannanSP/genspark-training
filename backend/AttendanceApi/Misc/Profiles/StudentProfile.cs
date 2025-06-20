using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using AutoMapper;

namespace AttendanceApi.Misc.Profiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<AddStudentRequestDTO, Student>();
    }
}