namespace assignment_2.Models
{
    public class AppointmentSearchModel
    {
        public string? PatientName { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public Range<int>? AgeRange { get; set; }

        public void GetSearchParamsFromUser()
        {
            PatientName = ConsoleInput.GetStringFromUser("Patient Name", true);
            var appointmentOffset = ConsoleInput.GetIntFromUser("Appointment in how many days? ", true);
            if (appointmentOffset == null)
                AppointmentDate = null;
            else
                AppointmentDate = DateTime.Now.AddDays(appointmentOffset!.Value);
            AgeRange = ConsoleInput.GetIntRangeFromUser("Age");
        }
    } 
}
