using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;
    public AttendanceController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    [Authorize]
    [HttpGet]
    [Route("{studentId}")]
    public async Task<ActionResult<List<SessionAttendance>>> GetAttendanceOfStudent(int studentId)
    {
        var students = await _attendanceService.GetAttendanceOfStudent(studentId);
        return Ok(students);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<ActionResult<SessionAttendance>> MarkAttendanceToStudent(int studentId, int sessionId)
    {
        var attendance = await _attendanceService.AddAttendanceToStudent(studentId, sessionId);
        return Created("", attendance);
    }

    [Authorize(Roles = "Teacher")]
    [HttpGet]
    [Route("Session/{sesssionId}")]
    public async Task<ActionResult<List<SessionAttendance>>> GetAttendanceBySession(int sesssionId)
    {
        var attendance = await _attendanceService.GetAttendanceOfSession(sesssionId);
        return Ok(attendance);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    [Route("Unmark")]
    public async Task<ActionResult<SessionAttendance>> UnmarkAttendanceToStudent(int studentId, int sessionId)
    {
        var attendance = await _attendanceService.RemoveAttendanceFromStudent(studentId, sessionId);
        return Ok(attendance);
    }
}