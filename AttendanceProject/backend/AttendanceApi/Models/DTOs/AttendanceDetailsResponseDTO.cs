namespace AttendanceApi.Models.DTOs;

public class AttendanceDetailsResponseDTO
{
    public int SessionId { get; set; }
    public string SessionName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int RegisteredCount { get; set; }
    public int AttendedCount { get; set; }
}