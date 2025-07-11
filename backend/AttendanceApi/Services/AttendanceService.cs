using System.Threading.Tasks;
using AttendanceApi.Interfaces;
using AttendanceApi.Misc;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using Microsoft.AspNetCore.SignalR;
using QuestPDF.Fluent;

namespace AttendanceApi.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IRepository<int, SessionAttendance> _sessionAttendanceRepository;
    private readonly IRepository<int, Session> _sessionRepository;

    private readonly IHubContext<NotificationHub> _hubContext;
    public AttendanceService(IRepository<int, SessionAttendance> sessionAttendanceRepository, IRepository<int, Session> sessionRepository, IHubContext<NotificationHub> hubContext)
    {
        _sessionAttendanceRepository = sessionAttendanceRepository;
        _sessionRepository = sessionRepository;
        _hubContext = hubContext;
    }
    public async Task<SessionAttendance> AddAttendanceToStudent(AttendanceUpdateDTO attendanceUpdateDTO)
    {
        var attendances = await _sessionAttendanceRepository.GetAll();
        var attendance = attendances.FirstOrDefault(s => s.StudentId == attendanceUpdateDTO.StudentId && s.SessionId == attendanceUpdateDTO.SessionId);
        if (attendance == null)
        {
            throw new Exception("Student not scheduled to attend session");
        }
        if (attendance.Status == "Attended")
            throw new Exception("Attendance already marked");
        if (attendance.Session.Status == "Cancelled")
            throw new Exception("Cannot add attendance to a cancelled session");

        attendance.Status = "Attended";
        attendance = await _sessionAttendanceRepository.Update(attendance.SessionAttendanceId, attendance);

        var connections = NotificationHub.GetConnections(attendance.Student.Email);
        if (connections != null)
        {
            foreach (var connection in connections)
            {
                await _hubContext.Clients.Client(connection).SendAsync("AttendanceMarked", attendance.Session.SessionName);
            }
        }

        return attendance;
    }

    public async Task<PaginatedResponseDTO<SessionAttendanceResponseDTO>> GetAttendanceOfSession(int sessionId, int page, int pageSize, string? studentName = null, bool? attended = null)
    {
        page = page > 0 ? page : 1;
        pageSize = pageSize > 0 ? pageSize : 10;

        var attendances = await _sessionAttendanceRepository.GetAll();
        attendances = attendances.Where(s => s.SessionId == sessionId);

        var response = attendances.Select(a => new SessionAttendanceDTO() { StudentId = a.StudentId, StudentName = a.Student.Name, Attended = a.Status == "Attended", SessionId = a.SessionId });

        if (!string.IsNullOrWhiteSpace(studentName))
            response = response.Where(a => a.StudentName.ToLower().Contains(studentName.ToLower()));

        if (attended.HasValue)
            response = response.Where(a => a.Attended == attended);

        var totalRecords = response.Count();
        var paginatedAttendance = response.OrderBy(s => s.StudentName).Skip((page - 1) * pageSize).Take(pageSize).ToList();

        var paginatedResponse = new PaginatedResponseDTO<SessionAttendanceResponseDTO>
        {
            Data = new SessionAttendanceResponseDTO
            {
                RegisteredCount = response.Count(),
                AttendedCount = response.Where(a => a.Attended).Count(),
                SessionAttendance = paginatedAttendance
            },
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

    public async Task<List<SessionAttendance>> GetAttendanceOfStudent(int studentId)
    {
        var attendances = await _sessionAttendanceRepository.GetAll();
        attendances = attendances.Where(s => s.StudentId == studentId && s.Session.Status != "Cancelled");

        return attendances.ToList();
    }

    public async Task<SessionAttendance> RemoveAttendanceFromStudent(AttendanceUpdateDTO attendanceUpdateDTO)
    {
        var attendances = await _sessionAttendanceRepository.GetAll();
        var attendance = attendances.FirstOrDefault(s => s.StudentId == attendanceUpdateDTO.StudentId && s.SessionId == attendanceUpdateDTO.SessionId);
        if (attendance == null)
        {
            throw new Exception("Student not scheduled to attend session");
        }
        if (attendance.Status == "NotAttended")
            throw new Exception("Attendance already unmarked");

        attendance.Status = "NotAttended";
        attendance = await _sessionAttendanceRepository.Update(attendance.SessionAttendanceId, attendance);

        var connections = NotificationHub.GetConnections(attendance.Student.Email);
        if (connections != null)
        {
            foreach (var connection in connections)
            {
                await _hubContext.Clients.Client(connection).SendAsync("AttendanceUnmarked", attendance.Session.SessionName);
            }
        }

        return attendance;
    }

    public async Task<byte[]> GenerateSessionReport(int sessionId)
    {
        var attendanceReportDto = new AttendanceReportDTO();

        var session = await _sessionRepository.Get(sessionId);
        if (session == null)
            throw new Exception("Session not found");
        attendanceReportDto.SessionName = session.SessionName;
        attendanceReportDto.Date = session.Date;
        attendanceReportDto.StartTime = session.StartTime;
        attendanceReportDto.EndTime = session.EndTime;

        var attendances = await _sessionAttendanceRepository.GetAll();
        attendances = attendances.Where(s => s.SessionId == sessionId);
        var attendanceDetails = attendances.Select(a => new SessionAttendanceDTO() { StudentId = a.StudentId, StudentName = a.Student.Name, Attended = a.Status == "Attended", SessionId = a.SessionId, Email = a.Student.Email });
        attendanceReportDto.RegisteredCount = attendanceDetails.Count();
        attendanceReportDto.AttendedCount = attendanceDetails.Count(s => s.Attended == true);
        attendanceReportDto.SessionAttendance = attendanceDetails.OrderBy(s => s.StudentName).ToList();

        var document = new AttendanceReport(attendanceReportDto);
        using var stream = new MemoryStream();
        document.GeneratePdf(stream);
        return stream.ToArray();
    }
}