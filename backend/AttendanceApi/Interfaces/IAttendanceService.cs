using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;

namespace AttendanceApi.Interfaces;

public interface IAttendanceService
{
    public Task<SessionAttendance> AddAttendanceToStudent(AttendanceUpdateDTO attendanceUpdateDTO);
    public Task<SessionAttendance> RemoveAttendanceFromStudent(AttendanceUpdateDTO attendanceUpdateDTO);
    public Task<List<SessionAttendance>> GetAttendanceOfStudent(int studentId);
    public Task<PaginatedResponseDTO<SessionAttendanceResponseDTO>> GetAttendanceOfSession(int sessionId, int page, int pageSize, string? studentName = null, bool? attended = null);
}