namespace AttendanceApi.Models.DTOs;

public class StudentDetailsDTO
{
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Gender { get; set; }
}