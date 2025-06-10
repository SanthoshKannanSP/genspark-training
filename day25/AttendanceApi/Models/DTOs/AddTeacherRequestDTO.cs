namespace AttendanceApi.Models.DTOs;

public class AddTeacherRequestDTO
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public List<AddSpecialityRequestDTO> Specialities { get; set; } = [];
}