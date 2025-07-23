namespace AttendanceApi.Models.DTOs;

public class BatchResponseDto
{
    public int BatchId { get; set; }
    public string BatchName { get; set; } = string.Empty;
    public List<BatchStudentDto> Students { get; set; } = new();
}