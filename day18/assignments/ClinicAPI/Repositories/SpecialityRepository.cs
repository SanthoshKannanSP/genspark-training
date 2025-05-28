using Microsoft.EntityFrameworkCore;

public  class SpecialityRepository : AbstractRepository<int, Speciality>
{
    public SpecialityRepository(ClinicContext clinicContext) : base(clinicContext)
    {
    }

    public override async Task<Speciality> Get(int key)
    {
        var speciality = await _clinicContext.Specialities.SingleOrDefaultAsync(p => p.Id == key);

        return speciality??throw new Exception("No speciality with the given ID");
    }

    public override async Task<IEnumerable<Speciality>> GetAll()
    {
        var specialitys = _clinicContext.Specialities;
        if (specialitys.Count() == 0)
            throw new Exception("No Specialitys in the database");
        return await specialitys.ToListAsync();
    }
}