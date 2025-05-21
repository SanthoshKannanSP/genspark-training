using System.Xml.Linq;
using assignment_2.Interfaces;
using assignment_2.Models;

namespace assignment_2.Services
{
    public class AppointmentService : IAppointmentService
    {
        IRepository<int, Appointment> _appointmentRepository;

        public AppointmentService(IRepository<int, Appointment> appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public int AddAppointment(Appointment appointment)
        {
            try
            {
                var result = _appointmentRepository.Add(appointment);
                if (result != null)
                {
                    return result.Id;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return -1;
        }

        public List<Appointment>? SearchAppointments(AppointmentSearchModel appointmentSearchModel)
        {
            try
            {
                var appointments = _appointmentRepository.GetAll();
                appointments = SearchByName(appointments, appointmentSearchModel.PatientName);
                appointments = SeachByAge(appointments, appointmentSearchModel.AgeRange);
                appointments = SearchByDate(appointments, appointmentSearchModel.AppointmentDate);
                if (appointments != null && appointments.Count > 0)
                    return appointments.ToList(); ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null; ;
        }


        private ICollection<Appointment> SearchByDate(ICollection<Appointment> appointments, DateTime? appointmentDate)
        {
            if (appointmentDate == null || appointments.Count == 0 || appointments == null)
            {
                return appointments;
            }
            else
            {
                return appointments.Where(e => e.AppointmentDate.Date == appointmentDate!.Value.Date).ToList();
            }
        }

        private ICollection<Appointment> SeachByAge(ICollection<Appointment> appointments, Range<int>? ageRange)
        {
            if (ageRange == null || appointments.Count == 0 || appointments == null)
            {
                return appointments;
            }
            else
            {
                return appointments.Where(e => e.PatientAge >= ageRange.MinVal && e.PatientAge <= ageRange.MaxVal).ToList();
            }
        }

        private ICollection<Appointment> SearchByName(ICollection<Appointment> appointments, string? patientName)
        {
            if (patientName == null || appointments.Count == 0 || appointments == null)
            {
                return appointments;
            }
            else
            {
                return appointments.Where(e => e.PatientName.ToLower().Contains(patientName.ToLower())).ToList();
            }
        }
    }
}
