using System.Threading.Tasks;
using AttendanceApi.Interfaces;
using AttendanceApi.Misc;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace AttendanceApi.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IRepository<int, Models.SessionAttendance> _sessionAttendanceRepository;
    private readonly IHubContext<NotificationHub> _hubContext;
    public AttendanceService(IRepository<int, Models.SessionAttendance> sessionAttendanceRepository, IHubContext<NotificationHub> hubContext)
    {
        _sessionAttendanceRepository = sessionAttendanceRepository;
        _hubContext = hubContext;
    }
    public async Task<Models.SessionAttendance> AddAttendanceToStudent(int studentId, int sessionId)
    {
        var attendances = await _sessionAttendanceRepository.GetAll();
        var attendance = attendances.FirstOrDefault(s => s.StudentId == studentId && s.SessionId == sessionId);
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
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", attendance.Student.Name, attendance.Session.SessionName);
        return attendance;
    }

    public async Task<PaginatedResponseDTO<SessionAttendanceResponseDTO>> GetAttendanceOfSession(int sessionId, int page, int pageSize, string? studentName = null, bool? attended = null)
    {
        page = page > 0 ? page : 1;
        pageSize = pageSize > 0 ? pageSize : 10;

        var attendances = await _sessionAttendanceRepository.GetAll();
        attendances = attendances.Where(s => s.SessionId == sessionId);

        var response = attendances.Select(a => new SessionAttendanceDTO() { StudentId = a.StudentId, StudentName = a.Student.Name, Attended = a.Status == "Attended" });

        if (!string.IsNullOrWhiteSpace(studentName))
            response = response.Where(a => a.StudentName.ToLower().Contains(studentName.ToLower()));

        if (attended.HasValue)
            response = response.Where(a => a.Attended==attended);

        var totalRecords = response.Count();
        var paginatedAttendance = response.Skip((page - 1) * pageSize).Take(pageSize).ToList();

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

    public async Task<List<Models.SessionAttendance>> GetAttendanceOfStudent(int studentId)
    {
        var attendances = await _sessionAttendanceRepository.GetAll();
        attendances = attendances.Where(s => s.StudentId == studentId && s.Session.Status!="Cancelled");

        return attendances.ToList();
    }

    public async Task<Models.SessionAttendance> RemoveAttendanceFromStudent(int studentId, int sessionId)
    {
        var attendances = await _sessionAttendanceRepository.GetAll();
        var attendance = attendances.FirstOrDefault(s => s.StudentId == studentId && s.SessionId == sessionId);
        if (attendance == null)
        {
            throw new Exception("Student not scheduled to attend session");
        }
        if (attendance.Status == "NotAttended")
            throw new Exception("Attendance already unmarked");

        attendance.Status = "NotAttended";
        attendance = await _sessionAttendanceRepository.Update(attendance.SessionAttendanceId, attendance);
        return attendance;
    }
}