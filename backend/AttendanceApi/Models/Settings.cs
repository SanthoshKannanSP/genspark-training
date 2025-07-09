namespace AttendanceApi.Models;

public class Settings
{
    public string Username { get; set; } = string.Empty;
    public string Theme { get; set; } = "light";
    public string DateFormat { get; set; } = "dd/MM/yyyy";
    public string TimeFormat { get; set; } = "hh:mm aa";

    public User? User { get; set; }
}