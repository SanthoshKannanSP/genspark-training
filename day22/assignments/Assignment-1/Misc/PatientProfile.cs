using AutoMapper;
using assignment_1.Models;
using assignment_1.Models.DTOs;

namespace assignment_1.Misc
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<PatientAddRequestDTO, Patient>();

            CreateMap<Patient, PatientAddRequestDTO>();
        }
    }
}