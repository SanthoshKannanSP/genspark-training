using AttendanceApi.Interfaces;
using AttendanceApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class NoteController : ControllerBase
{
    private readonly INotesService _noteService;
    public NoteController(INotesService notesService)
    {
        _noteService = notesService;
    }

    [HttpPost]
    public async Task<ActionResult> UploadNote([FromForm] UploadNoteDTO uploadNoteDTO)
    {
        if (uploadNoteDTO.File == null || uploadNoteDTO.File.Length == 0 || uploadNoteDTO.File.ContentType != "application/pdf")
            return BadRequest("Invalid Request");

        await _noteService.UploadFile(uploadNoteDTO);

        return Ok();
    }


    [HttpGet]
    [Route("{noteId}")]
    public async Task<ActionResult> RetrieveNote(int noteId)
    {
        var downloadNoteResponseDTO = await _noteService.DownloadFile(noteId);
        Response.Headers["Content-Disposition"] = $"inline; filename={downloadNoteResponseDTO.FileName}.pdf";

        return File(downloadNoteResponseDTO.Content, "application/pdf");
    }

    [HttpGet]
    [Route("Session/{sessionId}")]
    public async Task<ActionResult<List<SessionNotesResponseDTO>>> SessionNotes(int sessionId)
    {
        var sessionNotesResponseDTO = await _noteService.GetSessionNotes(sessionId);
        return Ok(sessionNotesResponseDTO);
    }

    [HttpDelete]
    [Route("{noteId}")]
    public async Task<ActionResult> DeleteNote(int noteId)
    {
        await _noteService.DeleteNote(noteId);
        return Ok();
    }
}