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

    [Authorize]
    [HttpGet]
    [Route("All")]
    public async Task<ActionResult<List<Session>>> GetAllSessions(int page,
    int pageSize,
    string? sessionName = null,
    DateOnly? startDate = null,
    DateOnly? endDate = null,
    TimeOnly? startTime = null,
    TimeOnly? endTime = null,
    string? status = null)
    {
        var sessions = await _sessionService.GetAllSession(page, pageSize, sessionName, startDate, endDate, startTime, endTime, status);
        return Ok(sessions);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<ActionResult<Session>> ScheduleSession(AddSessionRequestDTO addSessionRequestDTO)
    {
        Console.WriteLine(addSessionRequestDTO.SessionName);
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

    [Authorize(Roles = "Student")]
    [HttpPost]
    [Route("AddStudent")]
    public async Task<ActionResult<List<SessionAttendance>>> AddStudentToSession(AddStudentToSessionRequestDTO addStudentsToSessionRequestDTO)
    {
        var sessionAttendances = await _sessionService.AddStudentToSession(addStudentsToSessionRequestDTO);
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

    [Authorize(Roles = "Teacher, Student")]
    [HttpGet]
    [Route("Upcoming")]
    public async Task<ActionResult<List<UpcomingSessionsResponseDTO>>> GetUpcomingSessions()
    {
        var sessions = await _sessionService.GetUpcomingSessions();
        return Ok(sessions);
    }

    [Authorize(Roles = "Teacher, Student")]
    [HttpGet]
    [Route("Past")]
    public async Task<ActionResult<List<PastSessionResponseDTO>>> GetPastSessions()
    {
        var sessions = await _sessionService.GetPastSessions();
        return Ok(sessions);
    }

    [Authorize(Roles = "Teacher")]
    [HttpGet]
    [Route("Attendance")]
    public async Task<ActionResult<List<AttendanceDetailsResponseDTO>>> GetAttendanceDetails(int page,
    int pageSize,
    string? sessionName = null,
    DateOnly? startDate = null,
    DateOnly? endDate = null,
    TimeOnly? startTime = null,
    TimeOnly? endTime = null)
    {
        var sessions = await _sessionService.GetAttendanceDetails(page, pageSize, sessionName, startDate, endDate, startTime, endTime);
        return Ok(sessions);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    [Route("Make/Live")]
    public async Task<ActionResult<Session>> MakeSessionLive(SessionIdDTO sessionIdDTO)
    {
        var session = await _sessionService.MakeSessionLive(sessionIdDTO);
        return Ok(session);
    }

    [Authorize(Roles = "Teacher")]
    [HttpGet]
    [Route("Live")]
    public async Task<ActionResult<LiveSessionResponseDTO>> GetLiveSession()
    {
        var session = await _sessionService.GetLiveSession();
        return Ok(session);
    }
}