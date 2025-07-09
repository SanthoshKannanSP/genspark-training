using AttendanceApi.Models.DTOs;

namespace AttendanceApi.Interfaces;

public interface ISettingsService
{
    public Task<SettingsDTO> GetUserSettings();

    public Task<SettingsDTO> UpdateUserSettings(SettingsDTO settingsResponseDTO);
    public Task CreateDefaultSettings(string username);
}