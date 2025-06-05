using AutoMapper;
using assignment_1.Models;
using assignment_1.Models.DTOs;

namespace assignment_1.Misc
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<AppointmentAddRequestDTO, Appointment>();
        }
    }
}