namespace AttendanceApi.Models.DTOs;

public class AttendanceEditRequestDTO
{
    public int SessionAttendanceId { get; set; }
    public string RequestedStatus { get; set; } = "Attended";
}