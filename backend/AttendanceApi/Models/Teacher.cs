using AttendanceApi.Interfaces;

namespace AttendanceApi.Models;

public class Teacher : IOwnableResource
{
    public int TeacherId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public List<Session> Sessions { get; set; } = [];

    public User? User { get; set; }

    public string OwnerName => Email;
}