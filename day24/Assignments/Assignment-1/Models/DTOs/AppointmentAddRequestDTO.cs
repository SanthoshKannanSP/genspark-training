namespace assignment_1.Models.DTOs
{
    public class AppointmentAddRequestDTO
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
    }
}