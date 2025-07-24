using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BatchController : ControllerBase
{
    private readonly IBatchService _batchService;

    public BatchController(IBatchService batchService)
    {
        _batchService = batchService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _batchService.GetAllBatchesAsync();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _batchService.GetBatchByIdAsync(id);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BatchCreateRequestDto batchDto)
    {
        var result = await _batchService.CreateBatchAsync(batchDto);
        return CreatedAtAction(nameof(GetById), new { id = result.BatchId }, result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("assign-student")]
    public async Task<IActionResult> AssignStudentToBatch([FromBody] AssignStudentRequestDto dto)
    {
        var result = await _batchService.AssignStudentToBatchAsync(dto);
        // return Ok(result);
        return NoContent();
    }
}
