using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface IAttendanceService
{
    public SessionAttendance AddAttendanceToStudent(int studentId, int sessionId);
    public SessionAttendance RemoveAttendanceFromStudent(int studentId, int sessionId);
    public List<SessionAttendance> GetAttendanceOfStudent(int studentId);
    public List<SessionAttendance> GetAttendanceOfSession(int sessionId);
}