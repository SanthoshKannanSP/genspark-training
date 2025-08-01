using Backend.Interfaces;
using Backend.Models.DTOs.Color;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ColorController : ControllerBase
{
    private readonly IColorService _colorService;
    public ColorController(IColorService colorService)
    {
        _colorService = colorService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ColorResponseDTO>>> GetAllCategories()
    {
        var response = await _colorService.GetAllColors();
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<ColorResponseDTO>> GetColor(int id)
    {
        var response = await _colorService.GetColor(id);
        return Ok(response);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<ColorResponseDTO>> CreateColor(AddColorRequestDTO addColorRequestDTO)
    {
        var response = await _colorService.AddColor(addColorRequestDTO);
        return Ok(response);
    }

    [HttpPost]
    [Route("Edit")]
    public async Task<ActionResult<ColorResponseDTO>> EditColor(EditColorRequestDTO editColorRequestDTO)
    {
        var response = await _colorService.EditColor(editColorRequestDTO);
        return Ok(response);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<ActionResult> DeleteColor(int id)
    {
        var response = await _colorService.DeleteColor(id);
        return Ok(response);
    }
}