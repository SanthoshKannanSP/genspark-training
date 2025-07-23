namespace AttendanceApi.Models.DTOs;
public class AttendanceEditRequestDTOResponse
{
    public int Id { get; set; }
    public int SessionAttendanceId { get; set; }
    public string RequestedStatus { get; set; }
    public string Status { get; set; }
    public DateTime RequestedAt { get; set; }

    // Related SessionAttendance & Student
    public int StudentId { get; set; }
    public int SessionId { get; set; }
    public string StudentName { get; set; }
    public string SessionName { get; set; }
    public string CurrentStatus { get; set; }
}
