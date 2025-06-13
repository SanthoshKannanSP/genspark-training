using System.Threading.Tasks;
using AttendanceApi.Interfaces;
using AttendanceApi.Misc;
using AttendanceApi.Models;
using Microsoft.AspNetCore.SignalR;

namespace AttendanceApi.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IRepository<int, SessionAttendance> _sessionAttendanceRepository;
    private readonly IHubContext<NotificationHub> _hubContext;
    public AttendanceService(IRepository<int, SessionAttendance> sessionAttendanceRepository, IHubContext<NotificationHub> hubContext)
    {
        _sessionAttendanceRepository = sessionAttendanceRepository;
        _hubContext = hubContext;
    }
    public async Task<SessionAttendance> AddAttendanceToStudent(int studentId, int sessionId)
    {
        var attendances = await _sessionAttendanceRepository.GetAll();
        var attendance = attendances.FirstOrDefault(s => s.StudentId == studentId && s.SessionId == sessionId);
        if (attendance == null)
        {
            throw new Exception("Student not scheduled to attend session");
        }

        if (attendance.Status == "Attended")
            throw new Exception("Attendance already marked");

        attendance.Status = "Attended";
        attendance = await _sessionAttendanceRepository.Update(attendance.SessionAttendanceId, attendance);
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", attendance.Student.Name, attendance.Session.SessionName);
        return attendance;
    }

    public async Task<List<SessionAttendance>> GetAttendanceOfSession(int sessionId)
    {
        var attendances = await _sessionAttendanceRepository.GetAll();
        attendances = attendances.Where(s => s.SessionId == sessionId);

        return attendances.ToList();
    }

    public async Task<List<SessionAttendance>> GetAttendanceOfStudent(int studentId)
    {
        var attendances = await _sessionAttendanceRepository.GetAll();
        attendances = attendances.Where(s => s.StudentId == studentId);

        return attendances.ToList();
    }

    public async Task<SessionAttendance> RemoveAttendanceFromStudent(int studentId, int sessionId)
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