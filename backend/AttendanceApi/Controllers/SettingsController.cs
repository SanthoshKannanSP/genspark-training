using AttendanceApi.Interfaces;
using AttendanceApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly ISettingsService _settingsService;

    public SettingsController(ISettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<SettingsDTO>> getUserSettings()
    {
        var settings = await _settingsService.GetUserSettings();
        return Ok(settings);
    }

    [Authorize]
    [HttpPost]
    [Route("Update")]
    public async Task<ActionResult<SettingsDTO>> updateUserSettings(SettingsDTO settingsDTO)
    {
        var settings = await _settingsService.UpdateUserSettings(settingsDTO);
        return Ok(settings);
    }
}