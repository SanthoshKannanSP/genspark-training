using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class PatientController : ControllerBase
{
    static List<Patient> patients = new List<Patient>
    {
        new Patient{Id=101,Name="Ramu", PhoneNumber="1234567890", DateOfBirth=DateTime.Parse("1990-05-26")},
        new Patient{Id=102,Name="Somu", PhoneNumber="2134567890", DateOfBirth=DateTime.Parse("2003-09-02")},
    };
    [HttpGet]
    public ActionResult<IEnumerable<Patient>> GetPatients()
    {
        return Ok(patients);
    }
    [HttpPost]
    public ActionResult<Patient> PostPatient([FromBody] Patient patient)
    {
        patients.Add(patient);
        return Created("", patient);
    }

    [HttpPut]
    public ActionResult<Patient> UpdatePatient([FromBody] Patient patient)
    {
        var existingPatient = patients.FirstOrDefault(pat => pat.Id == patient.Id);
        if (existingPatient == null)
        {
            return NotFound();
        }

        existingPatient.Name = patient.Name;
        existingPatient.PhoneNumber = patient.PhoneNumber;
        existingPatient.DateOfBirth = patient.DateOfBirth;
        return Ok(patient);
    }

    [HttpDelete]
    public ActionResult<Patient> DeletePatient([FromBody] int id )
    {
        var existingPatient = patients.FirstOrDefault(pat => pat.Id == id);
        if (existingPatient == null)
        {
            return NotFound();
        }

        patients.Remove(existingPatient);
        return Ok(existingPatient);
    }
}