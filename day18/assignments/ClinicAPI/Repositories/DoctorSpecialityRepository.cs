using Microsoft.EntityFrameworkCore;

public  class DoctorSpecialityRepository : AbstractRepository<int, DoctorSpeciality>
    {
        public DoctorSpecialityRepository(ClinicContext clinicContext) : base(clinicContext)
        {
        }

        public override async Task<DoctorSpeciality> Get(int key)
        {
            var doctorSpeciality = await _clinicContext.DoctorSpecialities.SingleOrDefaultAsync(p => p.SerialNumber == key);

            return doctorSpeciality??throw new Exception("No doctor Speciality with the given ID");
        }

        public override async Task<IEnumerable<DoctorSpeciality>> GetAll()
        {
            var doctorSpecialitys = _clinicContext.DoctorSpecialities.Include(ds => ds.Doctor).Include(ds => ds.Speciality);
            if (doctorSpecialitys.Count() == 0)
                throw new Exception("No Doctor Specialitys in the database");
            return await doctorSpecialitys.ToListAsync();
        }
}