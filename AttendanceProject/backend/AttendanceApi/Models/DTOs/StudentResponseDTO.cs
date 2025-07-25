namespace AttendanceApi.Models.DTOs;

public class StudentResponseDto
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }

    public BatchDto? Batch { get; set; }

    public class BatchDto
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; }
    }
}
