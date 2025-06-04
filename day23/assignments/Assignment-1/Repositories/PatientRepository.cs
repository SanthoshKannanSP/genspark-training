using assignment_1.Contexts;
using assignment_1.Models;
using Microsoft.EntityFrameworkCore;

namespace assignment_1.Repositories
{
    public  class PatientRepository : AbstractRepository<int, Patient>
    {
        public PatientRepository(ClinicContext context) : base(context)
        {
        }

        public override async Task<Patient> Get(int key)
        {
            return await _clinicContext.Patients.SingleOrDefaultAsync(p => p.Id == key);
        }

        public override async Task<IEnumerable<Patient>> GetAll()
        {
            var patients = _clinicContext.Patients;
            if (patients.Count() == 0)
                throw new Exception("No Patients in the database");
            return await patients.ToListAsync();
        }
}
}