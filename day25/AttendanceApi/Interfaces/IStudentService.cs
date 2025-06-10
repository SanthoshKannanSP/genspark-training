using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;

namespace AttendanceApi.Interfaces;

public interface IStudentService
{
    public Task<Student> AddStudent(AddStudentRequestDTO addStudentRequestDTO);
    public Task<Student> DeactivateStudent(int StudentId);
    public Task<List<Student>> GetAllActiveStudents();
    public Task<Student> GetStudent(int studentId);
}