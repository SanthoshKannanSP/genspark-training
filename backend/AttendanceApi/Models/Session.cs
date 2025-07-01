using System.ComponentModel.DataAnnotations;
using AttendanceApi.Interfaces;

namespace AttendanceApi.Models;

public class Session : IOwnableResource
{
    public int SessionId { get; set; }
    public string SessionName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Status { get; set; } = string.Empty; // Scheduled, Completed, Cancelled
    public string TeacherEmail { get; set; } = string.Empty;
    public string SessionLink { get; set; } = string.Empty;
    public string SessionCode { get; set; } = string.Empty;

    public Teacher? MadeBy { get; set; }
    public List<SessionAttendance> StudentAttendance { get; set; }

    public string OwnerName => TeacherEmail;
}