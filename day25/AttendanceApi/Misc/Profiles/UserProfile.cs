using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using AutoMapper;

namespace AttendanceApi.Misc.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AddTeacherRequestDTO, User>()
            .ForMember(dest => dest.Username, opts => opts.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opts => opts.Ignore());

        CreateMap<AddStudentRequestDTO, User>()
            .ForMember(dest => dest.Username, opts => opts.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opts => opts.Ignore());
    }
}