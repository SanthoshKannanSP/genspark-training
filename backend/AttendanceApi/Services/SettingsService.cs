using System.Security.Claims;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;

namespace AttendanceApi.Services;

public class SettingsService : ISettingsService
{
    private readonly IRepository<string, Settings> _settingsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public SettingsService(IRepository<string, Settings> settingsRepository, IHttpContextAccessor httpContextAccessor)
    {
        _settingsRepository = settingsRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task CreateDefaultSettings(string username)
    {
        var settings = new Settings();
        settings.Username = username;
        settings = await _settingsRepository.Add(settings);
        return;
    }

    public async Task<SettingsDTO> GetUserSettings()
    {
        var username = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
        if (username == null)
            throw new Exception("Username not found");

        var settings = await _settingsRepository.Get(username);
        return new SettingsDTO()
        {
            Theme = settings.Theme,
            DateFormat = settings.DateFormat,
            TimeFormat = settings.TimeFormat
        };
    }

    public async Task<SettingsDTO> UpdateUserSettings(SettingsDTO settingsResponseDTO)
    {
        var username = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
        if (username == null)
            throw new Exception("Username not found");

        var settings = await _settingsRepository.Get(username);
        settings.Theme = settingsResponseDTO.Theme;
        settings.DateFormat = settingsResponseDTO.DateFormat;
        settings.TimeFormat = settingsResponseDTO.TimeFormat;
        settings = await _settingsRepository.Update(settings.Username, settings);
        return new SettingsDTO()
        {
            Theme = settings.Theme,
            DateFormat = settings.DateFormat,
            TimeFormat = settings.TimeFormat
        };
    }
}