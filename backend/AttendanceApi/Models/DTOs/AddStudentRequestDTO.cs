using System.ComponentModel.DataAnnotations;

namespace AttendanceApi.Models.DTOs;

public class AddStudentRequestDTO
{
    [MinLength(1, ErrorMessage = "Enter a valid Name")]
    public string Name { get; set; } = string.Empty;
    [Range(5,100, ErrorMessage = "Enter a valid Age")]
    public int Age { get; set; }
    [AllowedValues(["Male","Female"], ErrorMessage = "Should be either Male or Female only")]
    public string Gender { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [MinLength(8, ErrorMessage = "Password should be atleast 8 letters")]
    public string Password { get; set; } = string.Empty;
}