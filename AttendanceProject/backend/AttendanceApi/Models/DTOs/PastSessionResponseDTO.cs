using AttendanceApi.Models.DTOs;

public class PastSessionResponseDTO
{
    public int SessionId { get; set; }
    public string SessionName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool Attended { get; set; }
    public TeacherDetails? TeacherDetails { get; set; }
}