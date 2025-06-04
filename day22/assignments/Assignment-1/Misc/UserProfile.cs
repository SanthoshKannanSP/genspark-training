using AutoMapper;
using assignment_1.Models;
using assignment_1.Models.DTOs;

namespace assignment_1.Misc
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<DoctorAddRequestDTO, User>()
            .ForMember(dest => dest.Username, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.Password, opt => opt.Ignore());
           
            CreateMap<PatientAddRequestDTO, User>()
            .ForMember(dest => dest.Username, act => act.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }
}