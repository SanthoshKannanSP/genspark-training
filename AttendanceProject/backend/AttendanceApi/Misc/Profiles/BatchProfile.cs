using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using AutoMapper;

namespace AttendanceApi.Misc.Profiles;

public class BatchProfile : Profile
{
    public BatchProfile()
    {
        CreateMap<Batch, BatchResponseDto>();
        CreateMap<BatchCreateRequestDto, Batch>();
        CreateMap<Student, BatchStudentDto>();
    }
}