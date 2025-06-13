using System.ComponentModel.DataAnnotations;

namespace AttendanceApi.Models.DTOs;

public class LoginRequestDTO
{
    [EmailAddress(ErrorMessage = "Enter a valid username")]
    public string Username { get; set; } = string.Empty;
    [MinLength(1, ErrorMessage = "Enter a valid password")]
    public string Password { get; set; } = string.Empty;
}