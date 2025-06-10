using AttendanceApi.Interfaces;

namespace AttendanceApi.Models;

public class Student : IOwnableResource
{
    public int StudentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    public string OwnerName => Email;

    public List<SessionAttendance> SessionsToAttend = [];
    public User? User;
}