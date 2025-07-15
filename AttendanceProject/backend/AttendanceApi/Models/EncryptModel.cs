namespace AttendanceApi.Models;

public class EncryptModel
{
    public string? Data { get; set; } = string.Empty;
    public byte[]? EncryptedData = [];
    public byte[]? HashKey = [];
}