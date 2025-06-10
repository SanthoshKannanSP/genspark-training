using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;

namespace AttendanceApi.Interfaces;

public interface ITeacherService
{
    public Task<Teacher> AddTeacher(AddTeacherRequestDTO addTeacherRequestDTO);
    public Task<Teacher> DeactivateTeacher(int teacherId);
    public Task<List<Teacher>> GetAllActiveTeachers();
    public Task<Teacher> GetTeacher(int teacherId);
}