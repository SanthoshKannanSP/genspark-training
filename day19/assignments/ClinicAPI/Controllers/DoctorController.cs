using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet]
    public async Task<ActionResult<Doctor>> GetDoctor(string name)
    {
        try
        {
            var doctor = await _doctorService.GetDoctByName(name);
            return Ok(doctor);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<ActionResult<Doctor>> CreateDoctor([FromBody] DoctorAddRequestDTO doctor)
    {
        var createDoctor = await _doctorService.AddDoctor(doctor);
        return Created("", doctor);
    }

    [HttpGet]
    [Route("Specialities")]
    public async Task<ActionResult<IEnumerable<DoctorsBySpecialityResponseDto>>> GetDoctors(string speciality)
    {
        var result = await _doctorService.GetDoctorsBySpeciality(speciality);
        return Ok(result);
    }
}