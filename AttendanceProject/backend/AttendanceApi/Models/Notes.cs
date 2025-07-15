namespace AttendanceApi.Models;

public class Notes
{
    public int NoteId { get; set; }
    public string NoteName { get; set; } = string.Empty;

    public int SessionId { get; set; }
    public string NoteCode { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    public Session Session { get; set; }
}