using assignment_1.Interfaces;
using assignment_1.Models;
using assignment_1.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assignment_1.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> AddAppointment([FromBody] AppointmentAddRequestDTO requestDTO)
        {
            var appointment = await _appointmentService.AddAppointment(requestDTO);
            return Created("", appointment);
        }

        [HttpGet]
        public async Task<ActionResult<List<Appointment>>> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointments();
            return Ok(appointments);
        }

        [Authorize(Policy = "AtleastExperience10")]
        [HttpDelete]
        public async Task<ActionResult<Appointment>> CancelAppointment(int appointmentNumber)
        {
            var appointment = await _appointmentService.CancelAppointment(appointmentNumber);
            return Ok(appointment);
        }
    }
}