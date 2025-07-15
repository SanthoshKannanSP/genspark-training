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
    private readonly IRepository<int, Student> _studentRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IMapper _mapper;
    public SessionService(IRepository<int, Session> sessionRepository, IRepository<int, SessionAttendance> sessionAttendanceRepository, IRepository<int,Student> studentRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _sessionRepository = sessionRepository;
        _sessionAttendanceRepository = sessionAttendanceRepository;
        _studentRepository = studentRepository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }
    public async Task<List<SessionAttendance>> AddStudentToSession(AddStudentToSessionRequestDTO addStudentsToSessionRequestDTO)
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrWhiteSpace(username))
            throw new Exception("username not found");
        var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
        if (string.IsNullOrWhiteSpace(role) || role != "Student")
            throw new Exception("Invalid role");

        var sessions = await _sessionRepository.GetAll();
        var session = sessions.FirstOrDefault(s => s.SessionCode == addStudentsToSessionRequestDTO.SessionCode);
        if (session == null)
        {
            throw new Exception("Session not found");
        }
        var students = await _studentRepository.GetAll();
        var student = students.FirstOrDefault(s => s.Email == username);
        if (student == null)
        {
            throw new Exception("Student not found");
        }

        List<SessionAttendance> sessionAttendances = new();
            var sessionAttendance = new SessionAttendance() { StudentId = student.StudentId, SessionId = session.SessionId, Status = "NotAttended" };
            sessionAttendance = await _sessionAttendanceRepository.Add(sessionAttendance);
            sessionAttendances.Add(sessionAttendance);
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

    public async Task<PaginatedResponseDTO<List<AllSessionRequestDTO>>> GetAllSession(
    int page,
    int pageSize,
    string? sessionName = null,
    DateOnly? startDate = null,
    DateOnly? endDate = null,
    TimeOnly? startTime = null,
    TimeOnly? endTime = null,
    string? status = null)
    {

        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(username))
            throw new Exception("Username nor found");
        var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
        if (string.IsNullOrEmpty(role))
            throw new Exception("Role not found");

        page = page > 0 ? page : 1;
        pageSize = pageSize > 0 ? pageSize : 10;

        IQueryable<AllSessionRequestDTO> allSessions;
        if (role == "Teacher")
        {
            var sessions = await _sessionRepository.GetAll();
            allSessions = sessions.Where(s => s.TeacherEmail == username).Select(s => new AllSessionRequestDTO()
            {
                SessionId = s.SessionId,
                SessionName = s.SessionName,
                SessionLink = s.SessionLink,
                SessionCode = s.SessionCode,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Date = s.Date,
                Status = s.Status,
            });

        }
        else if (role == "Student")
        {
            var sessionAttendances = await _sessionAttendanceRepository.GetAll();
            allSessions = sessionAttendances.Where(s => s.Student.Email == username).Select(s => new AllSessionRequestDTO()
            {
                SessionId = s.Session.SessionId,
                SessionName = s.Session.SessionName,
                StartTime = s.Session.StartTime,
                EndTime = s.Session.EndTime,
                Date = s.Session.Date,
                Status = s.Session.Status,
                Attended = s.Status == "Attended"
            });
        }
        else
            throw new Exception("Unknown role");


        // Apply filters
        if (!string.IsNullOrWhiteSpace(sessionName))
            allSessions = allSessions.Where(s => s.SessionName.ToLower().Contains(sessionName.ToLower()));

        if (startDate.HasValue)
            allSessions = allSessions.Where(s => s.Date >= startDate.Value);

        if (endDate.HasValue)
            allSessions = allSessions.Where(s => s.Date <= endDate.Value);

        if (startTime.HasValue)
            allSessions = allSessions.Where(s => s.StartTime >= startTime.Value);

        if (endTime.HasValue)
            allSessions = allSessions.Where(s => s.EndTime <= endTime.Value);

        if (!string.IsNullOrWhiteSpace(status))
            allSessions = allSessions.Where(s => s.Status.ToLower() == status.ToLower());

        var totalRecords = allSessions.Count();
        var paginatedSessions = allSessions.OrderByDescending(s => s.Date).ThenByDescending(s => s.StartTime).ThenByDescending(s => s.EndTime).Skip((page - 1) * pageSize).Take(pageSize).ToList();

        var response = new PaginatedResponseDTO<List<AllSessionRequestDTO>>
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


    public async Task<List<PastSessionResponseDTO>> GetPastSessions()
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(username))
            throw new Exception("Username not found");
        var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
        if (string.IsNullOrEmpty(role))
            throw new Exception("Role not found");

        var response = new List<PastSessionResponseDTO>();
        if (role == "Teacher")
        {
            var sessions = await _sessionRepository.GetAll();
            sessions = sessions.Where(s => s.TeacherEmail == username && s.Status == "Completed").OrderByDescending(s => s.Date).ThenByDescending(s => s.SessionId).Take(3);
            foreach (var session in sessions)
            {
                var responseDto = new PastSessionResponseDTO()
                {
                    SessionId = session.SessionId,
                    SessionName = session.SessionName,
                    Date = session.Date,
                    StartTime = session.StartTime,
                    EndTime = session.EndTime,
                    Status = session.Status
                };
                response.Add(responseDto);
            }
        }
        else if (role == "Student")
        {
            var attendances = await _sessionAttendanceRepository.GetAll();
            attendances = attendances.Where(s => s.Student.Email == username && s.Session.Status == "Completed").OrderByDescending(s => s.Session.Date).ThenByDescending(s => s.SessionId).Take(3);
            var sessions = attendances.Select(s => s.Session);

            for (int index = 0; index < sessions.Count(); index++)
            {
                var teacherDetails = new TeacherDetails()
                {
                    TeacherId = sessions.ElementAt(index).MadeBy.TeacherId,
                    TeacherName = sessions.ElementAt(index).MadeBy.Name,
                    Organization = sessions.ElementAt(index).MadeBy.Organization,
                };
                var responseDto = new PastSessionResponseDTO()
                {
                    SessionId = sessions.ElementAt(index).SessionId,
                    SessionName = sessions.ElementAt(index).SessionName,
                    Date = sessions.ElementAt(index).Date,
                    StartTime = sessions.ElementAt(index).StartTime,
                    EndTime = sessions.ElementAt(index).EndTime,
                    Status = sessions.ElementAt(index).Status,
                    Attended = attendances.ElementAt(index).Status == "Attended",
                    TeacherDetails = teacherDetails
                };
                response.Add(responseDto);
            }
        }

        return response;
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

    public async Task<List<UpcomingSessionsResponseDTO>> GetUpcomingSessions()
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(username))
            throw new Exception("Username not found");
        var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
        if (string.IsNullOrEmpty(role))
            throw new Exception("Role not found");

        var response = new List<UpcomingSessionsResponseDTO>();
        if (role == "Teacher")
        {
            var sessions = await _sessionRepository.GetAll();
            sessions = sessions.Where(s => s.MadeBy.Email == username && s.Status == "Scheduled" && s.Date >= DateOnly.FromDateTime(DateTime.Now)).OrderBy(s => s.Date).ThenBy(s => s.StartTime).Take(3);

            foreach (var session in sessions)
            {
                var responseDto = new UpcomingSessionsResponseDTO()
                {
                    SessionId = session.SessionId,
                    SessionName = session.SessionName,
                    SessionLink = session.SessionLink,
                    SessionCode = session.SessionCode,
                    Date = session.Date,
                    StartTime = session.StartTime,
                    EndTime = session.EndTime,
                    Status = session.Status
                };
                response.Add(responseDto);
            }
        }
        else if (role == "Student")
        {
            var attendances = await _sessionAttendanceRepository.GetAll();
            var sessions = attendances.Where(s => s.Student.Email == username && s.Session.Date >= DateOnly.FromDateTime(DateTime.Now)  && (s.Session.Status == "Scheduled" || s.Session.Status == "Live")).OrderBy(s => s.Session.Date).ThenBy(s => s.Session.StartTime).Take(3).Select(s => s.Session);

            foreach (var session in sessions)
            {
                var teacherDetails = new TeacherDetails()
                {
                    TeacherId = session.MadeBy.TeacherId,
                    TeacherName = session.MadeBy.Name,
                    Organization = session.MadeBy.Organization,
                };
                var responseDto = new UpcomingSessionsResponseDTO()
                {
                    SessionId = session.SessionId,
                    SessionName = session.SessionName,
                    SessionLink = session.SessionLink,
                    Date = session.Date,
                    StartTime = session.StartTime,
                    EndTime = session.EndTime,
                    Status = session.Status,
                    TeacherDetails = teacherDetails
                };
                response.Add(responseDto);
            }
        }

        return response;  
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
        session.SessionCode = GenerateSessionCode();

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
        session.SessionLink = updateSessionRequestDTO.SessionLink;
        session = await _sessionRepository.Update(session.SessionId, session);
        return session;
    }

    public async Task<PaginatedResponseDTO<List<AttendanceDetailsResponseDTO>>> GetAttendanceDetails(int page,
    int pageSize,
    string? sessionName = null,
    DateOnly? startDate = null,
    DateOnly? endDate = null,
    TimeOnly? startTime = null,
    TimeOnly? endTime = null)
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(username))
            throw new Exception("Username nor found");
        var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
        if (string.IsNullOrEmpty(role))
            throw new Exception("Role not found");

        page = page > 0 ? page : 1;
        pageSize = pageSize > 0 ? pageSize : 10;

        IQueryable<Session> sessions;
        if (role == "Teacher")
        {
            sessions = await _sessionRepository.GetAll();
            sessions = sessions.Where(s => s.TeacherEmail == username);

        }
        else if (role == "Student")
        {
            var sessionAttendances = await _sessionAttendanceRepository.GetAll();
            sessions = sessionAttendances.Where(s => s.Student.Email == username).Select(s => s.Session);
        }
        else
            throw new Exception("Unknown role");

        sessions = sessions.Where(s => s.Status == "Completed");


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

        

        var response = sessions.Select(s => new AttendanceDetailsResponseDTO()
        { 
            SessionId = s.SessionId,
            SessionName = s.SessionName,
            Date = s.Date,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            RegisteredCount = s.StudentAttendance.Count(),
            AttendedCount = s.StudentAttendance.Where(s => s.Status == "Attended").Count(),
        });

        var totalRecords = response.Count();
        var paginatedAttendance = response.OrderByDescending(s => s.Date).ThenByDescending(s => s.StartTime).ThenByDescending(s => s.EndTime).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        
        var paginatedResponse = new PaginatedResponseDTO<List<AttendanceDetailsResponseDTO>>
        {
            Data = paginatedAttendance,
            Pagination = new PaginationModel
            {
                TotalRecords = totalRecords,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize)
            }
        };

        return paginatedResponse;
    }

    public async Task<Session> MakeSessionLive(SessionIdDTO sessionIdDTO)
    {
        var session = await _sessionRepository.Get(sessionIdDTO.SessionId);
        if (session == null)
            throw new Exception("Session not found");
        if (session.Status != "Scheduled")
            throw new Exception("Unable to make session live");
        session.Status = "Live";
        session = await _sessionRepository.Update(session.SessionId, session);
        return session;
    }

    public async Task<LiveSessionResponseDTO> GetLiveSession()
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(username))
            throw new Exception("Username nor found");

        var response = new LiveSessionResponseDTO();
        var sessions = await _sessionRepository.GetAll();
        var liveSession = sessions.FirstOrDefault(s => s.Status == "Live");
        if (liveSession == null)
            return response;

        response.SessionId = liveSession.SessionId;
        response.SessionName = liveSession.SessionName;
        var attendances = await _sessionAttendanceRepository.GetAll();
        attendances = attendances.Where(a => a.SessionId == liveSession.SessionId);
        response.AttendingStudents = attendances.Where(a => a.Status == "Attended").Select(a => new LiveSessionStudentsDTO()
        {
            StudentId = a.StudentId,
            StudentName = a.Student.Name
        }).OrderBy(a => a.StudentName).ToList();
        response.NotJoinedStudents = attendances.Where(a => a.Status == "NotAttended").Select(a => new LiveSessionStudentsDTO()
        {
            StudentId = a.StudentId,
            StudentName = a.Student.Name
        }).OrderBy(a => a.StudentName).ToList();
        return response;
    }


    private string GenerateSessionCode()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[8];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
    }
}