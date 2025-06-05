using AutoMapper;
using assignment_1.Models;
using assignment_1.Models.DTOs;

namespace assignment_1.Misc
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<DoctorAddRequestDTO, Doctor>();

            CreateMap<Doctor, DoctorAddRequestDTO>();
        }
    }
}