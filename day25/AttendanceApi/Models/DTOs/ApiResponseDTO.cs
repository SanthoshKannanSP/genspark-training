namespace AttendanceApi.Models.DTOs;

public class ApiResponseDTO
{
    public bool Success { get; set; }
    public object Data { get; set; }
    public string ErrorMessage { get; set; }
}