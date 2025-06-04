using System.ComponentModel.DataAnnotations;

namespace assignment_1.Models.DTOs
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "Username is manditory")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is manditory")]
        public string Password { get; set; } = string.Empty;
    }
}