namespace AttendanceApi.Models;

public class User
{
    public string Username { get; set; }
    public string Role { get; set; }
    public byte[] Password { get; set; }
    public byte[] HashKey { get; set; }

    public Teacher? Teacher { get; set; }
    public Student? Student { get; set; }
}