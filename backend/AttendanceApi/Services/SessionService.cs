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
    private readonly IMapper _mapper;
    public SessionService(IRepository<int, Session> sessionRepository, IRepository<int, SessionAttendance> sessionAttendanceRepository, IMapper mapper)
    {
        _sessionRepository = sessionRepository;
        _sessionAttendanceRepository = sessionAttendanceRepository;
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

    public async Task<List<Session>> GetAllSession(int page, int pageSize)
    {
        page = page > 0 ? page : 1;
        pageSize = pageSize > 0 ? pageSize : 10;
        var sessions = await _sessionRepository.GetAll();
        sessions = sessions.Skip((page - 1) * pageSize).Take(pageSize);
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
        sessions = sessions.Where(s => s.TeacherId == teacherId);
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