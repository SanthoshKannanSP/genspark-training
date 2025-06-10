namespace AttendanceApi.Models.DTOs;

public class AddStudentsToSessionRequestDTO
{
    public int SessionId { get; set; }
    public List<int> StudentIds { get; set; } = [];
}