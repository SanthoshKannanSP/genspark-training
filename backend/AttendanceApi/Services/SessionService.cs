using System.Security.Claims;
using System.Threading.Tasks;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using AutoMapper;

namespace AttendanceApi.Services;

public class SessionService : ISessionService
{
    private readonly IRepository<int, Session> _sessionRepository;
    private readonly IRepository<int, SessionAttendance> _sessionAttendanceRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IMapper _mapper;
    public SessionService(IRepository<int, Session> sessionRepository, IRepository<int, SessionAttendance> sessionAttendanceRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _sessionRepository = sessionRepository;
        _sessionAttendanceRepository = sessionAttendanceRepository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }
    public async Task<List<SessionAttendance>> AddStudentsToSession(AddStudentsToSessionRequestDTO addStudentsToSessionRequestDTO, int sessionId)
    {
        List<SessionAttendance> sessionAttendances = new();
        foreach (var studentId in addStudentsToSessionRequestDTO.StudentIds)
        {
            var sessionAttendance = new SessionAttendance() { StudentId = studentId, SessionId = sessionId, Status = "NotAttended" };
            sessionAttendance = await _sessionAttendanceRepository.Add(sessionAttendance);
            sessionAttendances.Add(sessionAttendance);
        }
        return sessionAttendances;
    }

    public async Task<Session> CancelSession(int sessionId)
    {
        var session = await _sessionRepository.Get(sessionId);
        if (session == null)
            throw new Exception("Session not found");
        session.Status = "Cancelled";
        session = await _sessionRepository.Update(session.SessionId, session);
        return session;
    }

    public async Task<Session> CompleteSession(int sessionId)
    {
        var session = await _sessionRepository.Get(sessionId);
        if (session == null)
            throw new Exception("Session not found");
        session.Status = "Completed";
        session = await _sessionRepository.Update(session.SessionId, session);
        return session;
    }

    public async Task<PaginatedResponseDTO<List<Session>>> GetAllSession(
    int page,
    int pageSize,
    string? sessionName = null,
    DateOnly? startDate = null,
    DateOnly? endDate = null,
    TimeOnly? startTime = null,
    TimeOnly? endTime = null,
    string? status = null)
{
    page = page > 0 ? page : 1;
    pageSize = pageSize > 0 ? pageSize : 10;

    var sessions = await _sessionRepository.GetAll();

    // Apply filters
    if (!string.IsNullOrWhiteSpace(sessionName))
        sessions = sessions.Where(s => s.SessionName.ToLower().Contains(sessionName.ToLower()));

    if (startDate.HasValue)
        sessions = sessions.Where(s => s.Date >= startDate.Value);

    if (endDate.HasValue)
        sessions = sessions.Where(s => s.Date <= endDate.Value);

    if (startTime.HasValue)
        sessions = sessions.Where(s => s.StartTime >= startTime.Value);

    if (endTime.HasValue)
        sessions = sessions.Where(s => s.EndTime <= endTime.Value);

    if (!string.IsNullOrWhiteSpace(status))
        sessions = sessions.Where(s => s.Status.ToLower() == status.ToLower());

    var totalRecords = sessions.Count();
    var paginatedSessions = sessions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

    var response = new PaginatedResponseDTO<List<Session>>
    {
        Data = paginatedSessions,
        Pagination = new PaginationModel
        {
            TotalRecords = totalRecords,
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize)
        }
    };

    return response;
}


    public async Task<List<Session>> GetPastSessions()
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        var sessions = await _sessionRepository.GetAll();
        sessions = sessions.Where(s => s.TeacherEmail == username && s.Status == "Completed").OrderByDescending(s => s.Date).ThenByDescending(s => s.SessionId).Take(3);
        return sessions.ToList();
    }

    public async Task<Session> GetSession(int sessionId)
    {
        var session = await _sessionRepository.Get(sessionId);
        return session;
    }

    public async Task<List<Session>> GetSessionByTeacher(int teacherId)
    {
        var sessions = await _sessionRepository.GetAll();
        sessions = sessions.Where(s => s.MadeBy.TeacherId == teacherId);
        return sessions.ToList();
    }

    public async Task<List<Session>> GetUpcomingSessions()
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        var sessions = await _sessionRepository.GetAll();
        sessions = sessions.Where(s => s.MadeBy.Email == username && s.Status == "Scheduled").OrderByDescending(s => s.Date).ThenByDescending(s => s.SessionId).Take(3);
        return sessions.ToList();
    }

    public async Task RemoveStudentsFromSession(List<int> studentIds, int sessionId)
    {
        var sessionAttendances = await _sessionAttendanceRepository.GetAll();
        sessionAttendances = sessionAttendances.Where(s => s.SessionId == sessionId && studentIds.Contains(s.StudentId));
        foreach (var sessionAttendance in sessionAttendances)
        {
            await _sessionAttendanceRepository.Delete(sessionAttendance.SessionAttendanceId);
        }
        return;
    }

    public async Task<Session> ScheduleSession(AddSessionRequestDTO addSessionRequestDTO)
    {
        var session = _mapper.Map<Session>(addSessionRequestDTO);
        session.Status = "Scheduled";
        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        session.TeacherEmail = username;

        session = await _sessionRepository.Add(session);
        return session;
    }

    public async Task<Session> UpdateSession(UpdateSessionRequestDTO updateSessionRequestDTO, int sessionId)
    {
        var session = await _sessionRepository.Get(sessionId);
        session.SessionName = updateSessionRequestDTO.SessionName;
        session.Date = updateSessionRequestDTO.Date;
        session.StartTime = updateSessionRequestDTO.StartTime;
        session.EndTime = updateSessionRequestDTO.EndTime;
        session = await _sessionRepository.Update(session.SessionId, session);
        return session;
    }
}