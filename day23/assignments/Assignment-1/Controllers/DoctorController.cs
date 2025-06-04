using assignment_1.Interfaces;
using assignment_1.Misc;
using assignment_1.Models;
using assignment_1.Models.DTOs;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authorization;
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
            return Created("", doctor);
        }

        [HttpGet]
        [GoogleScopedAuthorize]
        public async Task<ActionResult<List<Doctor>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctors();
            return Ok(doctors);
        }

        [CustomExceptionFilter]
        [HttpGet]
        [Route("name")]
        public async Task<ActionResult<Doctor>> DoctorByName(string name)
        {
            return await _doctorService.GetDoctorByName(name);
        }
    }
}