using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;
    public AttendanceController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    [Authorize]
    [HttpGet]
    [Route("{studentId}")]
    public async Task<ActionResult<List<Models.SessionAttendance>>> GetAttendanceOfStudent(int studentId)
    {
        var students = await _attendanceService.GetAttendanceOfStudent(studentId);
        return Ok(students);
    }

    [Authorize(Roles = "Teacher, Admin")]
    [HttpPost("Mark")]
    public async Task<ActionResult<SessionAttendance>> MarkAttendanceToStudent(AttendanceUpdateDTO attendanceUpdateDTO)
    {
        Console.WriteLine(attendanceUpdateDTO.SessionId);
        System.Console.WriteLine(attendanceUpdateDTO.StudentId);
        var attendance = await _attendanceService.AddAttendanceToStudent(attendanceUpdateDTO);
        return Created("", attendance);
    }

    [Authorize(Roles = "Teacher, Admin")]
    [HttpGet]
    [Route("Session/{sesssionId}")]
    public async Task<ActionResult<PaginatedResponseDTO<SessionAttendanceDTO>>> GetAttendanceBySession(int sesssionId, int page, int pageSize, string? studentName = null, bool? attended = null)
    {
        var attendance = await _attendanceService.GetAttendanceOfSession(sesssionId, page, pageSize, studentName, attended);
        return Ok(attendance);
    }

    [Authorize(Roles = "Teacher, Admin")]
    [HttpPost]
    [Route("Unmark")]
    public async Task<ActionResult<SessionAttendance>> UnmarkAttendanceToStudent(AttendanceUpdateDTO attendanceUpdateDTO)
    {
        var attendance = await _attendanceService.RemoveAttendanceFromStudent(attendanceUpdateDTO);
        return Ok(attendance);
    }

    [Authorize(Roles = "Teacher, Admin")]
    [HttpGet]
    [Route("{sessionId}/Report")]
    public async Task<ActionResult> GenerateAttendanceReport(int sessionId)
    {
        var report = await _attendanceService.GenerateSessionReport(sessionId);
        Response.Headers["Content-Disposition"] = "inline; filename=SessionReport.pdf";

        return File(report, "application/pdf");
    }

    [Authorize(Roles = "Teacher, Admin")]
    [HttpPost("request-edit")]
    [Route("request-edit")]
    public async Task<IActionResult> RequestEdit([FromBody] AttendanceEditRequestDTO dto)
    {
        var result = await _attendanceService.RequestAttendanceEditAsync(dto);
        return Ok(result);
    }

    // GET: api/AttendanceEditRequest
    [Authorize(Roles = "Admin")]
    [HttpGet("attendance-edit-requests")]
    public async Task<ActionResult<List<AttendanceEditRequestDTOResponse>>> GetAll()
    {
        var result = await _attendanceService.GetAllAttendanceEditRequests();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}/approve")]
    public async Task<IActionResult> ApproveEditRequest(int id)
    {
        var success = await _attendanceService.ApproveEditRequestAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}