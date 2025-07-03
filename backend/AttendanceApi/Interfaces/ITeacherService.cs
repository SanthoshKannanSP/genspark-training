using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;

namespace AttendanceApi.Interfaces;

public interface ITeacherService
{
    public Task<Teacher> AddTeacher(AddTeacherRequestDTO addTeacherRequestDTO);
    public Task<Teacher> DeactivateTeacher();
    public Task<List<Teacher>> GetAllActiveTeachers(int? page, int? pageSize);
    public Task<Teacher> GetTeacher(int teacherId);

    public Task<TeacherDetailsDTO> GetMyDetails();
    public Task<TeacherDetailsDTO> UpdateDetails(TeacherDetailsDTO teacherDetailsDTO);
}