namespace assignment_1.Models.DTOs
{
    public class DoctorAddRequestDTO
    {
        public string Name { get; set; } = string.Empty;
        public float YearsOfExperience { get; set; }
        public string Password { get; set; }
    }
}