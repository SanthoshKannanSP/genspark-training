using assignment_2.Models;

namespace assignment_2.Interfaces
{
    internal interface IAppointmentService
    {
        int AddAppointment(Appointment appointment);
        List<Appointment>? SearchAppointments(AppointmentSearchModel appointmentSearchModel);
    }
}
