using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class DoctorController : ControllerBase
{
    static List<Doctor> doctors = new List<Doctor>
    {
        new Doctor{Id=101,Name="Ramu"},
        new Doctor{Id=102,Name="Somu"},
    };
    [HttpGet]
    public ActionResult<IEnumerable<Doctor>> GetDoctors()
    {
        return Ok(doctors);
    }
    [HttpPost]
    public ActionResult<Doctor> PostDoctor([FromBody] Doctor doctor)
    {
        doctors.Add(doctor);
        return Created("", doctor);
    }

    [HttpPut]
    public ActionResult<Doctor> UpdateDoctor([FromBody] Doctor doctor)
    {
        var existingDoc = doctors.FirstOrDefault(doc => doc.Id == doctor.Id);
        if (existingDoc == null)
        {
            return NotFound();
        }

        existingDoc.Name = doctor.Name;
        return Ok(doctor);
    }

    [HttpDelete]
    public ActionResult<Doctor> DeleteDoctor([FromBody] int id )
    {
        var existingDoc = doctors.FirstOrDefault(doc => doc.Id == id);
        if (existingDoc == null)
        {
            return NotFound();
        }

        doctors.Remove(existingDoc);
        return Ok(existingDoc);
    }
}