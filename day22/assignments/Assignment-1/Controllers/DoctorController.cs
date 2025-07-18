using assignment_1.Interfaces;
using assignment_1.Models;
using assignment_1.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace assignment_1.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        public async Task<ActionResult<Doctor>> AddDoctor([FromBody] DoctorAddRequestDTO requestDTO)
        {
            var doctor = await _doctorService.AddDoctor(requestDTO);
            return Created("",doctor);
        }

        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctors();
            return Ok(doctors);
        }
    }
}