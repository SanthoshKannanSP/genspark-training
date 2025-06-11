using System.Threading.Tasks;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;

namespace AttendanceApi.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IRepository<int, SessionAttendance> _sessionAttendanceRepository;
    public AttendanceService(IRepository<int, SessionAttendance> sessionAttendanceRepository)
    {
        _sessionAttendanceRepository = sessionAttendanceRepository;
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