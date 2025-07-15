namespace AttendanceApi.Models.DTOs;

public class UploadNoteDTO
{
    public required IFormFile File { get; set; }
    public required int SessionId { get; set; }
}