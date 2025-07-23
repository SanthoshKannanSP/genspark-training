namespace AttendanceApi.Models;

public class AttendanceEditRequest
{
    public int Id { get; set; }
    public int SessionAttendanceId { get; set; }
    public string RequestedStatus { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;


    // N A V I G A T I O N
    public SessionAttendance SessionAttendance { get; set; }
}
