using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface IAttendanceService
{
    public Task<SessionAttendance> AddAttendanceToStudent(int studentId, int sessionId);
    public Task<SessionAttendance> RemoveAttendanceFromStudent(int studentId, int sessionId);
    public Task<List<SessionAttendance>> GetAttendanceOfStudent(int studentId);
    public Task<List<SessionAttendance>> GetAttendanceOfSession(int sessionId);
}