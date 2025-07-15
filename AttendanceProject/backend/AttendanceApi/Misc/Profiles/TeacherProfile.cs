using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using AutoMapper;

namespace AttendanceApi.Misc.Profiles;

public class TeacherProfile : Profile
{
    public TeacherProfile()
    {
        CreateMap<AddTeacherRequestDTO, Teacher>();
    }
}