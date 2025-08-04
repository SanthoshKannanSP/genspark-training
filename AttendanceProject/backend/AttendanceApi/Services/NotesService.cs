using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using AttendanceApi.Repositories;
using Azure.Storage.Blobs;

namespace AttendanceApi.Services;

public class NotesService : INotesService
{
    private readonly BlobContainerClient _containerClinet;
    private readonly IRepository<int, Notes> _noteRepository;
    public NotesService(IRepository<int, Notes> noteRepository, IConfiguration configuration)
    {
        _noteRepository = noteRepository;
        var keyVaultUrl = configuration["Azure:KeyVaultUrl"];
        var secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
        KeyVaultSecret secret = await secretClient.GetSecretAsync("ContainerSasUrl");
        var sasUrl = secret.Value;
        _containerClinet = new BlobContainerClient(new Uri(sasUrl!));
    }

    public async Task DeleteNote(int noteId)
    {
        var note = await _noteRepository.Get(noteId);
        if (note == null)
            throw new Exception("Note not found");

        if (note.IsDeleted)
            throw new Exception("Note already deleted");

        note.IsDeleted = true;
        await _noteRepository.Update(noteId, note);
        return;
    }

    public async Task<RetrieveNoteResponseDTO?> DownloadFile(int noteId)
    {
        var note = await _noteRepository.Get(noteId);
        if (note == null)
            throw new Exception("Note not found");
        if (note.IsDeleted)
            throw new Exception("Note is deleted");

        var blobClient = _containerClinet?.GetBlobClient(note.NoteCode);
        if (await blobClient!.ExistsAsync())
        {
            var downloadInfor = await blobClient.DownloadStreamingAsync();
            return new RetrieveNoteResponseDTO()
            {
                Content = downloadInfor.Value.Content,
                FileName = note.NoteName
            };
        }
        return null;
    }

    public async Task<List<SessionNotesResponseDTO>> GetSessionNotes(int sessionId)
    {
        var notes = await _noteRepository.GetAll();
        List<SessionNotesResponseDTO> responseDTOs = notes.Where(n => n.SessionId == sessionId && !n.IsDeleted).Select(n => new SessionNotesResponseDTO()
        {
            NoteId = n.NoteId,
            NoteName = n.NoteName,
            SessionId = n.SessionId
        }).ToList();
        return responseDTOs;
    }

    public async Task UploadFile(UploadNoteDTO uploadNoteDTO)
    {
        var noteCode = GenerateNoteCode() + ".pdf";
        var note = new Notes()
        {
            SessionId = uploadNoteDTO.SessionId,
            NoteName = Path.GetFileName(uploadNoteDTO.File.FileName),
            NoteCode = noteCode,
            IsDeleted = false
        };

        var blobClient = _containerClinet.GetBlobClient(noteCode);
        Stream file = uploadNoteDTO.File.OpenReadStream();
        await blobClient.UploadAsync(file);

        await _noteRepository.Add(note);

        file.Close();
        return;
    }

    private string GenerateNoteCode()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[10];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
    }
}
