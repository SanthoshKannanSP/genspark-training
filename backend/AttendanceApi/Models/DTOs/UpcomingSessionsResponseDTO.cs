namespace AttendanceApi.Models.DTOs;

public class UpcomingSessionsResponseDTO
{
    public int SessionId { get; set; }
    public string SessionName { get; set; } = string.Empty;
    public string SessionLink { get; set; } = string.Empty;
    public string? SessionCode { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Status { get; set; } = string.Empty;
    public TeacherDetails? TeacherDetails { get; set; }
}

public class TeacherDetails {
    public int TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
}