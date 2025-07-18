namespace Notify.Models.DTOs;

public class UserAddRequestDTO
{
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}