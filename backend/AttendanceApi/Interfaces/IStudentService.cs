using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;

namespace AttendanceApi.Interfaces;

public interface IStudentService
{
    public Task<Student> AddStudent(AddStudentRequestDTO addStudentRequestDTO);
    public Task<Student> DeactivateStudent();
    public Task<List<Student>> GetAllActiveStudents(int page, int pageSize);
    public Task<Student> GetStudent(int studentId);

    public Task<StudentDetailsDTO> GetMyDetails();
    public Task<StudentDetailsDTO> UpdateDetails(StudentDetailsDTO studentDetailsDTO);

}