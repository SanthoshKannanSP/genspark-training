namespace assignment_2.Models
{
    public class Appointment
    {
        private static int IdSequence = 1;
        public int Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public int PatientAge { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; } = string.Empty;

        public Appointment()
        {
            Id = IdSequence++;
        }

        public Appointment(string patientName, int patientAge, DateTime appointmentDate, string reason) : base()
        {
            PatientName = patientName;
            PatientAge = patientAge;
            AppointmentDate = appointmentDate;
            Reason = reason;
        }

        public void GetAppointmentDetailsFromUser()
        {   
            PatientName = ConsoleInput.GetStringFromUser("Patient Name")!;
            PatientAge = ConsoleInput.GetIntFromUser("Patient Age")!.Value;
            int appointmentOffset = ConsoleInput.GetIntFromUser("Appointment in how many days?")!.Value;
            AppointmentDate = DateTime.Now.AddDays(appointmentOffset);
            Reason = ConsoleInput.GetStringFromUser("Appointment Reason")!;
        }
        
        public override string ToString()
        {
            return $"Patient Id: {Id}\nPatient Name: {PatientName}\nPatient Age: {PatientAge}\nAppoint Date: {AppointmentDate.Date.ToString("d")}\nReason: {Reason}";
        }
    }
}
