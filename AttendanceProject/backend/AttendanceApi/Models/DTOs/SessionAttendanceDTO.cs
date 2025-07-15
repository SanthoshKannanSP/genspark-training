namespace AttendanceApi.Models.DTOs;

public class SessionAttendanceDTO
{
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public int SessionId { get; set; }
    public bool Attended { get; set; }
}