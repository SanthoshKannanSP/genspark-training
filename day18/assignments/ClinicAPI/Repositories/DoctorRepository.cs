using Microsoft.EntityFrameworkCore;

public class DoctorRepository : AbstractRepository<int, Doctor>
{
    public  DoctorRepository(ClinicContext clinicContext) : base(clinicContext) { }

    public override async Task<Doctor> Get(int key)
    {
        var doctor = await _clinicContext.Doctors.SingleOrDefaultAsync(p => p.Id == key);

        return doctor??throw new Exception("No Doctor with the given ID");
    }

    public override async Task<IEnumerable<Doctor>> GetAll()
    {
        var doctors = _clinicContext.Doctors.Include(d => d.DoctorSpecialities);
        if (doctors.Count() == 0)
            throw new Exception("No Doctors in the database");
        return await doctors.ToListAsync();
    }
}