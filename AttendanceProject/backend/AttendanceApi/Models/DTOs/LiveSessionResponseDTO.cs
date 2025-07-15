namespace AttendanceApi.Models.DTOs;

public class LiveSessionResponseDTO
{
    public int SessionId { get; set; } = -1;
    public string SessionName { get; set; } = string.Empty;
    public List<LiveSessionStudentsDTO> AttendingStudents { get; set; } = new();
    public List<LiveSessionStudentsDTO> NotJoinedStudents { get; set; } = new();
}

public class LiveSessionStudentsDTO
{
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
}