using System.Threading.Tasks;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Student>>> GetAllStudents(int page, int pageSize)
    {
        var students = await _studentService.GetAllActiveStudents(page, pageSize);
        return Ok(students);
    }


    [HttpGet]
    [Route("{studentId}")]
    public async Task<ActionResult<Student>> GetStudent(int studentId)
    {
        var student = await _studentService.GetStudent(studentId);
        return student;
    }

    [HttpPost]
    public async Task<ActionResult<Student>> AddStudent(AddStudentRequestDTO addStudentRequestDTO)
    {
        var student = await _studentService.AddStudent(addStudentRequestDTO);
        return Created("", student);
    }

    [Authorize(Roles = "Student")]
    [HttpDelete]
    public async Task<ActionResult<Student>> DeactivateStudent()
    {
        var student = await _studentService.DeactivateStudent();
        return student;
    }

    [Authorize(Roles = "Student")]
    [HttpGet]
    [Route("Me")]
    public async Task<ActionResult<StudentDetailsDTO>> GetMyDetails()
    {
        var student = await _studentService.GetMyDetails();
        return student;
    }

    [Authorize(Roles = "Student")]
    [HttpPost]
    [Route("Update")]
    public async Task<ActionResult<StudentDetailsDTO>> UpdateDetails(StudentDetailsDTO studentDetailsDTO)
    {
        var teacher = await _studentService.UpdateDetails(studentDetailsDTO);
        return teacher;
    }
}