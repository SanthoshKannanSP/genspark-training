namespace assignment_1.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PhoneNumber { get; set; }
        public byte[] HashKey { get; set; }
    }
}