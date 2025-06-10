using System.Security.Claims;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessionService;
    private readonly ITeacherService _teacherService;
    public SessionController(ISessionService sessionService, ITeacherService teacherService)
    {
        _sessionService = sessionService;
        _teacherService = teacherService;
    }

    [HttpGet]
    [Route("{sessionId}")]
    public async Task<ActionResult<Session>> GetSession(int sessionId)
    {
        var session = await _sessionService.GetSession(sessionId);
        return session;
    }

    [HttpGet]
    [Route("All")]
    public async Task<ActionResult<List<Session>>> GetAllSessions()
    {
        var sessions = await _sessionService.GetAllSession();
        return Ok(sessions);
    }

    [HttpPost]
    public async Task<ActionResult<Session>> ScheduleSession(AddSessionRequestDTO addSessionRequestDTO)
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        var teachers = await _teacherService.GetAllActiveTeachers();
        var teacherId = teachers.FirstOrDefault(t => t.Email == username)?.TeacherId;
        var session = await _sessionService.ScheduleSession(addSessionRequestDTO, teacherId);
        return Created("", session);
    }

    [HttpPost]
    [Route("{sessionId}/Cancel")]
    public async Task<ActionResult<Session>> CancelSession(int sessionId)
    {
        var session = await _sessionService.CancelSession(sessionId);
        return Ok(session);
    }

    [HttpPost]
    [Route("{sessionId}/Complete")]
    public async Task<ActionResult<Session>> CompleteSession(int sessionId)
    {
        var session = await _sessionService.CompleteSession(sessionId);
        return session;
    }

    [HttpPost]
    [Route("Student/Add")]
    public async Task<ActionResult<List<SessionAttendance>>> AddStudentToSession(AddStudentsToSessionRequestDTO addStudentsToSessionRequestDTO)
    {
        var sessionAttendances = await _sessionService.AddStudentsToSession(addStudentsToSessionRequestDTO);
        return Created("", sessionAttendances);
    }

    [HttpGet]
    [Route("Teacher/{teacherId}")]
    public async Task<ActionResult<List<Session>>> GetSessionsByTeacher(int teacherId)
    {
        var sessions = await _sessionService.GetSessionByTeacher(teacherId);
        return Ok(sessions);
    }

    [HttpPost]
    [Route("Update")]
    public async Task<ActionResult<Session>> UpdateSession(UpdateSessionRequestDTO updateSessionRequestDTO)
    {
        var session = await _sessionService.UpdateSession(updateSessionRequestDTO);
        return Ok(session);
    }
}