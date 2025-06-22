using System.Security.Claims;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AttendanceApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
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
    public async Task<ActionResult<List<Session>>> GetAllSessions(int page, int pageSize)
    {
        var sessions = await _sessionService.GetAllSession(page, pageSize);
        return Ok(sessions);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<ActionResult<Session>> ScheduleSession(AddSessionRequestDTO addSessionRequestDTO)
    {
        var session = await _sessionService.ScheduleSession(addSessionRequestDTO);
        return Created("", session);
    }

    [Authorize(Policy = "IsOwner")]
    [HttpPost]
    [Route("{sessionId}/Cancel")]
    public async Task<ActionResult<Session>> CancelSession(int sessionId)
    {
        var session = await _sessionService.CancelSession(sessionId);
        return Ok(session);
    }

    [Authorize(Policy = "IsOwner")]
    [HttpPost]
    [Route("{sessionId}/Complete")]
    public async Task<ActionResult<Session>> CompleteSession(int sessionId)
    {
        var session = await _sessionService.CompleteSession(sessionId);
        return session;
    }

    [Authorize(Policy = "IsOwner")]
    [HttpPost]
    [Route("{sessionId}/AddStudent")]
    public async Task<ActionResult<List<SessionAttendance>>> AddStudentToSession(AddStudentsToSessionRequestDTO addStudentsToSessionRequestDTO, int sessionId)
    {
        var sessionAttendances = await _sessionService.AddStudentsToSession(addStudentsToSessionRequestDTO, sessionId);
        return Created("", sessionAttendances);
    }

    [HttpGet]
    [Route("Teacher/{teacherId}")]
    public async Task<ActionResult<List<Session>>> GetSessionsByTeacher(int teacherId)
    {
        var sessions = await _sessionService.GetSessionByTeacher(teacherId);
        return Ok(sessions);
    }

    [Authorize(Policy = "IsOwner")]
    [HttpPost]
    [Route("{sessionId}/Update")]
    public async Task<ActionResult<Session>> UpdateSession(UpdateSessionRequestDTO updateSessionRequestDTO, int sessionId)
    {
        var session = await _sessionService.UpdateSession(updateSessionRequestDTO, sessionId);
        return Ok(session);
    }

    [Authorize(Roles = "Teacher")]
    [HttpGet]
    [Route("Upcoming")]
    public async Task<ActionResult<List<Session>>> GetUpcomingSessions()
    {
        var sessions = await _sessionService.GetUpcomingSessions();
        return sessions;
    }
}