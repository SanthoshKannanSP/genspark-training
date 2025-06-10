namespace AttendanceApi.Models.DTOs;

public class UpdateSessionRequestDTO
{
    public int SessionId { get; set; }
    public string SessionName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}