using assignment_1.Interfaces;
using assignment_1.Models;
using assignment_1.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assignment_1.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> AddPatient([FromBody] PatientAddRequestDTO requestDTO)
        {
            var user = await _patientService.AddPatient(requestDTO);
            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Patient")]
        public async Task<ActionResult<List<Patient>>> GetAllPatients()
        {
            var users = await _patientService.GetAllPatients();
            return Ok(users);
        }
    }
}