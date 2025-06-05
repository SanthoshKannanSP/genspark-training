using assignment_1.Models;
using assignment_1.Models.DTOs;

namespace assignment_1.Interfaces
{
    public interface IAppointmentService
    {
        public Task<Appointment> AddAppointment(AppointmentAddRequestDTO appointment);
        public Task<List<Appointment>> GetAllAppointments();
        public Task<Appointment> CancelAppointment(int key);
    }
}