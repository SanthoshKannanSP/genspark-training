namespace AttendanceApi.Models.DTOs;

public class AddSessionRequestDTO
{
    public string SessionName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}