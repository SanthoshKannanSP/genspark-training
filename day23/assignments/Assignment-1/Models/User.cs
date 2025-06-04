using System.ComponentModel.DataAnnotations;

namespace assignment_1.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public byte[] HashKey { get; set; }
        public string Role { get; set; }
        public byte[] Password { get; set; }

        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
    }
}