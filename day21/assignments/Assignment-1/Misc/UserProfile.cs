using AutoMapper;
using assignment_1.Models;
using assignment_1.Models.DTOs;

namespace assignment_1.Misc
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddRequestDTO, User>()
            .ForMember(dest => dest.Username, act => act.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.Ignore());
           
            CreateMap< User,UserAddRequestDTO>()
            .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Username));
        }
    }
}