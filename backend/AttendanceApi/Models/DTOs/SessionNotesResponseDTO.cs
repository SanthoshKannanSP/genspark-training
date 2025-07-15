namespace AttendanceApi.Models.DTOs;

public class SessionNotesResponseDTO
{
    public int NoteId { get; set; }
    public string NoteName { get; set; } = string.Empty;

    public int SessionId { get; set; }
}