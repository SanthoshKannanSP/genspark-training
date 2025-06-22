using System.ComponentModel.DataAnnotations;

namespace AttendanceApi.Models.DTOs;

public class AddTeacherRequestDTO
{
    [MinLength(1, ErrorMessage = "Enter a valid Name")]
    public string Name { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Organization { get; set; } = string.Empty;
    [MinLength(8, ErrorMessage = "Password should be atleast 8 letters")]
    public string Password { get; set; } = string.Empty;
}