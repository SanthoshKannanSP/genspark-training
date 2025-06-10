namespace AttendanceApi.Models;

public class SessionAttendance
{
    public int SessionAttendanceId { get; set; }
    public int SessionId { get; set; }
    public int StudentId { get; set; }
    public string Status { get; set; } = string.Empty; // NotAttended, Attended
    public Session Session { get; set; }
    public Student Student { get; set; }
}