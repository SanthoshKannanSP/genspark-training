namespace AttendanceApi.Models.DTOs;

public class LoginResponseDTO
{
    public string Username { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;

}