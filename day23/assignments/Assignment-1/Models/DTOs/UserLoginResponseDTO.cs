namespace assignment_1.Models.DTOs
{
    public class UserLoginResponseDTO
    {
        public string Username { get; set; } = string.Empty;
        public string? Token { get; set; }
    }
}