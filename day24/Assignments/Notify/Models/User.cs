namespace Notify.Models;

public class User
{
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public byte[] Password { get; set; } = [];
    public byte[] HashKey { get; set; } = [];
}