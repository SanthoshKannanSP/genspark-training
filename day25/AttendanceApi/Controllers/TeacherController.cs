using System.Threading.Tasks;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;
    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Teacher>>> GetAllTeachers()
    {
        var teachers = await _teacherService.GetAllActiveTeachers();
        return teachers;
    }

    [HttpGet]
    [Route("{teacherId}")]
    public async Task<ActionResult<Teacher>> GetTeacher(int teacherId)
    {
        var teacher = await _teacherService.GetTeacher(teacherId);
        return teacher;
    }

    [HttpPost]
    public async Task<ActionResult<Teacher>> AddTeacher(AddTeacherRequestDTO addTeacherRequestDTO)
    {
        var teacher = await _teacherService.AddTeacher(addTeacherRequestDTO);
        return Created("",teacher);
    }

    [Authorize(Policy = "IsOwner")]
    [HttpDelete]
    [Route("{teacherId}")]
    public async Task<ActionResult<Teacher>> DeactivateTeacher(int teacherId)
    {
        var teacher = await _teacherService.DeactivateTeacher(teacherId);
        return Ok(teacher);
    }
}