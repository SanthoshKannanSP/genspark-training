using AttendanceApi.Models.DTOs;

namespace AttendanceApi.Interfaces;

public interface INotesService
{
    public Task UploadFile(UploadNoteDTO uploadNoteDTO);

    public Task<RetrieveNoteResponseDTO> DownloadFile(int noteId);

    public Task<List<SessionNotesResponseDTO>> GetSessionNotes(int sessionId);
    public Task DeleteNote(int noteId);
}