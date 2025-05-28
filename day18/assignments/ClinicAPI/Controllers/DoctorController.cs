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
    public async Task<ActionResult<DoctorResponseDTO>> CreateDoctor([FromBody] DoctorAddRequestDTO doctor)
    {
        var createDoctor = await _doctorService.AddDoctor(doctor);
        var response = new DoctorResponseDTO() { Name = createDoctor.Name, Status = createDoctor.Status, YearsOfExperience = createDoctor.YearsOfExperience };
        return Created("", response);
    }

    [HttpGet]
    [Route("Specialities")]
    public async Task<ActionResult<ICollection<SpecialityResponseDTO>>> GetSpecialitiesOfDoctor(string doctorName)
    {
        var specialities = await _doctorService.GetSpecialitiesByDoctor(doctorName);
        var response = new List<SpecialityResponseDTO>();
        foreach (var speciality in specialities)
        {
            response.Add(new SpecialityResponseDTO() { Id = speciality.Id, Name = speciality.Name });
        }
        return Ok(response);
    }
}