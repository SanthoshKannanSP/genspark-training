using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using AutoMapper;

namespace AttendanceApi.Misc.Profiles;

public class SessionProfile : Profile
{
    public SessionProfile()
    {
        CreateMap<AddSessionRequestDTO, Session>();
    }
}