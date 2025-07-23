using AttendanceApi.Interfaces;

namespace AttendanceApi.Models;

public class Student : IOwnableResource
{
    public int StudentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    public string OwnerName => Email;


    public int? BatchId { get; set; }
    public Batch? Batch { get; set; }
    public List<SessionAttendance> SessionsToAttend = [];
    public User? User;
}