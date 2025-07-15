namespace AttendanceApi.Models.DTOs;

public class SessionAttendanceResponseDTO
{
    public int RegisteredCount { get; set; }
    public int AttendedCount { get; set; }
    public List<SessionAttendanceDTO> SessionAttendance { get; set; } = [];
}