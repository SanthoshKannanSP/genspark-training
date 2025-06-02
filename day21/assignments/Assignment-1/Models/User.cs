using System.ComponentModel.DataAnnotations;

namespace assignment_1.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public byte[] HashKey { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public byte[] Password { get; set; }
    }
}