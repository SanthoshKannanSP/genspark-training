namespace AttendanceApi.Models;

public class User
{
    public string Username { get; set; }
    public string Role { get; set; }
    public byte[] Password { get; set; }
    public byte[] HashKey { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public Teacher? Teacher { get; set; }
    public Student? Student { get; set; }
    public Settings? Settings { get; set; }
}