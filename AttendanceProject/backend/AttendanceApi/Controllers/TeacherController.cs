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
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;
        private readonly ISettingsService _settingsService;

    public TeacherController(ITeacherService teacherService, ISettingsService settingsService)
    {
        _teacherService = teacherService;
        _settingsService = settingsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Teacher>>> GetAllTeachers(int page, int pageSize)
    {
        var teachers = await _teacherService.GetAllActiveTeachers(page, pageSize);
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
        await _settingsService.CreateDefaultSettings(teacher.Email);
        return Created("", teacher);
    }

    [Authorize(Roles = "Teacher, Admin")]
    [HttpDelete]
    public async Task<ActionResult<Teacher>> DeactivateTeacher()
    {
        var teacher = await _teacherService.DeactivateTeacher();
        return Ok(teacher);
    }

    // ADMIN DELETE
    [HttpDelete("{teacherId}")]
    public async Task<IActionResult> DeactivateTeacherById(int teacherId)
    {
        var result = await _teacherService.DeleteTeacherByIdAsync(teacherId);
        return Ok(new { message = "Teacher deactivated successfully", success = result });
    }


    [Authorize(Roles = "Teacher, Admin")]
    [HttpGet]
    [Route("Me")]
    public async Task<ActionResult<TeacherDetailsDTO>> GetMyDetails()
    {
        var teacher = await _teacherService.GetMyDetails();
        return teacher;
    }

    [Authorize(Roles = "Teacher, Admin")]
    [HttpPost]
    [Route("Update")]
    public async Task<ActionResult<TeacherDetailsDTO>> UpdateDetails(TeacherDetailsDTO teacherDetailsDTO)
    {
        var teacher = await _teacherService.UpdateDetails(teacherDetailsDTO);
        return teacher;
    }
}