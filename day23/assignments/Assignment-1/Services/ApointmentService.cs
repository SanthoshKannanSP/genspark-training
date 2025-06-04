using System.Threading.Tasks;
using AutoMapper;
using assignment_1.Interfaces;
using assignment_1.Models;
using assignment_1.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace assignment_1.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<int, Appointment> _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IRepository<int, Appointment> appointmentRepository,
                            IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<Appointment> AddAppointment(AppointmentAddRequestDTO appointment)
        {
            try
            {
                var newAppointment = _mapper.Map<AppointmentAddRequestDTO, Appointment>(appointment);
                newAppointment.Status = "Active";
                newAppointment = await _appointmentRepository.Add(newAppointment);
                if (newAppointment == null)
                    throw new Exception("Could not add appointment");
                return newAppointment;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            try
            {
                var appointments = await _appointmentRepository.GetAll();
                return appointments.Where(a => a.Status == "Active").ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Appointment> CancelAppointment(int key)
        {
            try
            {
                var appointment = await _appointmentRepository.Get(key);
                if (appointment == null)
                    throw new Exception("Appointment not found");
                if (appointment.Status == "Cancelled")
                    throw new Exception("Appointment already cancelled");
                appointment.Status = "Cancelled";
                appointment = await _appointmentRepository.Update(key, appointment);
                return appointment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}