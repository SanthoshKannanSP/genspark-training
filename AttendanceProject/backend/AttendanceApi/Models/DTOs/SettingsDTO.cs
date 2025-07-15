namespace AttendanceApi.Models.DTOs;

public class SettingsDTO
{
    public string Theme { get; set; } = string.Empty;
    public string DateFormat { get; set; } = string.Empty;
    public string TimeFormat { get; set; } = string.Empty;
}